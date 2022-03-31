using NewApp.Models;
using NewApp.Services;

namespace NewApp;

public partial class NewsPage : ContentPage
{
    private bool IsNextPage = false;
    public List<Article> ArticleList { get; set; }
    public List<Category> CategoryList = new List<Category>()
    {
        new Category() { Name = "Breaking-news" },
        new Category() { Name = "World" },
        new Category() { Name = "Nation" },
        new Category() { Name = "Business" },
        new Category() { Name = "Technology" },
        new Category() { Name = "Entertainment" },
        new Category() { Name = "Sports" },
        new Category() { Name = "Science" },
        new Category() { Name = "Health" },
    };
	public NewsPage()
	{
		InitializeComponent();
        ArticleList = new List<Article>();
        CvCategories.ItemsSource = CategoryList;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!IsNextPage)
        {
            await PassCategory("breaking-news");
        }
        
   
    }

    public async Task PassCategory(string categoryName)
    {
        CvNews.ItemsSource = null;
        ArticleList.Clear();

        ApiService apiService = new ApiService();
        var newsResult = await apiService.GetNews(categoryName);
        foreach (var article in newsResult.Articles)
        {
            ArticleList.Add(article);
        }

        CvNews.ItemsSource = ArticleList;
    }

    private async void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectetCategory = e.CurrentSelection.FirstOrDefault() as Category;
        await PassCategory(selectetCategory.Name);
    }

    private void CvNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedArticle = e.CurrentSelection.FirstOrDefault() as Article;
        IsNextPage = true;
        Navigation.PushAsync(new NewsDetailPage(selectedArticle));
    }
}