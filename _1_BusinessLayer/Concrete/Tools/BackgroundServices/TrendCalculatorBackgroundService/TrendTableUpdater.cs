using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Abstractions.Interfaces;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repository;
using Microsoft.Extensions.Hosting;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.TrendCalculatorBackgroundService
{
    public class TrendTableUpdater : BackgroundService
    {
        private readonly AbstractGenericBaseCommandHandler _genericCommandHandler;
        private readonly AbstractPostQueryHandler _postQueryHandler;
        private readonly AbstractTrendingPostQueryHandler _trendingPostQueryHandler;
        public TrendTableUpdater(AbstractGenericBaseCommandHandler repository, AbstractPostQueryHandler postQueryHandler, AbstractTrendingPostQueryHandler _tr)
        {
            _postQueryHandler = postQueryHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var trendingPosts = new List<TrendingPost>();
                var posts = await _postQueryHandler.GetWithCustomSearchAsync(q => q.Select(p => new Post
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    LikeCount = p.LikeCount,
                    EntryCount = p.EntryCount,
                    DateTime = p.DateTime
                }));

                foreach (var post in posts)
                {
                    // Temel popülerlik skoru
                    double score = post.LikeCount * 2 + post.EntryCount * 3;

                    // 0 etkileşimli post için log hatası olmasın diye +1 ekliyoruz
                    score = Math.Max(score, 1);

                    // İçeriğin yaşı (saat cinsinden)
                    double ageHours = (DateTime.UtcNow - post.DateTime).TotalHours;

                    // Hot score hesaplama (Reddit tarzı)
                    var HotScore = Math.Log10(score) - (ageHours / 24);
                    // ageHours / 24 -> post eskiyse skor düşer, yeniyse yüksek
                    trendingPosts.Add(new TrendingPost
                    {
                        PostId = post.PostId,
                        PostTitle = post.Title,
                        HotScore = HotScore,
                        EntryCount = post.EntryCount,
                        DateTime = post.DateTime
                    });
                }

                await _genericCommandHandler.ManuallyInsertRangeAsync<TrendingPost>(trendingPosts);
                var oldTrends = await _trendingPostQueryHandler.GetWithCustomSearchAsync(q => q.OrderBy(t => t.DateTime));
                await _genericCommandHandler.DeleteRangeAsync<TrendingPost>(oldTrends);
                await _genericCommandHandler.SaveChangesAsync();



                await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken); // periyodik çalıştırma
            }
        }
    }
}
