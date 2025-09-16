using System;

namespace _2_DataAccessLayer.Concrete.Enums
{
    [Flags]
    public enum TopicTypes
    {
        None = 0,               // Hiçbir tür yok

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
