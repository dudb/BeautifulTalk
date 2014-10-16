using BeautifulTalk.Modules.Login.Models;
using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Login.Services.Client.Mocks
{
    public class MockCollectInterestCategoriesService : ICollectInterestCategoriesService
    {
        public InterestCategoryCollection CollectCategories()
        {
            InterestCategoryCollection Categories = new InterestCategoryCollection();
            Categories.Add(new InterestCategory("Art", "This is Art",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_art.png")));

            Categories.Add(new InterestCategory("Automobile", "This is Automobile",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_automobile.png")));

            Categories.Add(new InterestCategory("Building", "This is Building",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_building.png")));

            Categories.Add(new InterestCategory("Cloth", "This is Cloth",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_cloth.png")));

            Categories.Add(new InterestCategory("Dance", "This is Dance",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_dance.png")));

            Categories.Add(new InterestCategory("Donation", "This is Donation",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_donation.png")));

            Categories.Add(new InterestCategory("Food", "This is Food",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_food.png")));

            Categories.Add(new InterestCategory("Gamble", "This is Gamble",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_gamble.png")));

            Categories.Add(new InterestCategory("Mistery", "This is Mistery",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_mistery.png")));

            Categories.Add(new InterestCategory("Movie", "This is Movie",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_movie.png")));

            Categories.Add(new InterestCategory("Music", "This is Music",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_music.png")));

            Categories.Add(new InterestCategory("News", "This is News",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_news.png")));

            Categories.Add(new InterestCategory("Physics", "This is Physics",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_physics.png")));

            Categories.Add(new InterestCategory("Plant", "This is Plant",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_plant.png")));

            Categories.Add(new InterestCategory("Politics", "This is Politics",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_politics.png")));

            Categories.Add(new InterestCategory("Reading", "This is Reading",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_reading.png")));

            Categories.Add(new InterestCategory("Sports", "This is Sports",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_sports.png")));

            Categories.Add(new InterestCategory("Toy", "This is Toy",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_toy.png")));

            Categories.Add(new InterestCategory("Trip", "This is Trip",
                    ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Login;component/Resources/Images/base_trip.png")));
            return Categories;
        }
    }
}
