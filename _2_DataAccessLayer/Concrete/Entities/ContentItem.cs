using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    using System;
    using global::_2_DataAccessLayer.Concrete.Entities._2_DataAccessLayer.Concrete.Enums.OtherEnums;

    namespace _2_DataAccessLayer.Concrete.Enums.OtherEnums
    {
        [Flags]
        public enum TopicTypes
        {

            // Haber ve güncel konular
            Politics = 1,           // Siyaset
            Economy = 2,            // Ekonomi, finans
            WorldNews = 4,          // Dünya haberleri
            LocalNews = 8,          // Yerel haberler
            Trending = 16,          // Popüler / viral konular

            // Teknoloji ve bilim
            Technology = 32,        // Teknoloji
            Science = 64,           // Bilimsel gelişmeler
            AI = 128,               // Yapay zeka
            Space = 256,            // Uzay ve astronomi
            Health = 512,           // Sağlık ve tıp haberleri

            // Spor ve eğlence
            Sports = 1024,          // Spor etkinlikleri
            Entertainment = 2048,   // Sinema, dizi, müzik
            Gaming = 4096,          // Oyun ve e-spor
            Celebrity = 8192,       // Ünlüler

            // Kişisel ve sosyal
            Lifestyle = 16384,      // Yaşam tarzı, hobi
            Education = 32768,      // Eğitim, tutorial
            Relationships = 65536,  // İlişkiler, romantizm
        }
    }

    public class ContentItem
    {
        public Guid ContentItemId { get; set; }
        public Guid? ActorId { get; set; }
        public Actor? Actor { get; set; }
        public string? Content { get; set; }
        public int? LikeCount { get; set; }
        public int? EntryCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Entry>? ChildEntries { get; set; }
        public ICollection<ContextForBotItemChain>? ContextForBotItemChainsAsRoot { get; set; }
        public ICollection<ContextForBotItemChain>? ContextForBotItemChainsAsChild {  get; set; }

    }

    public class Post : ContentItem
    {
        public string? Title { get; set; }
        public TopicTypes? TopicTypes { get; set; }

    }

    public class Entry : ContentItem
    {
        public Guid? ParentContentId { get; set; }
        public ContentItem? ParentContent { get; set; }
    }

    public class ContextForBotItemChain
    {
        public Guid ContentItemChainId { get; set; }
        public Guid? RootContentItemId { get; set; }
        public ContentItem? RootContentItem { get; set; }
        public Guid? ChildContentItemId { get; set; }
        public ContentItem? ChildContentItem { get; set; }
    }
}
