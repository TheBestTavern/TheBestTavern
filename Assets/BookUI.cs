using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public enum BookType
{
    Dish,
    Ingredient,
    Mix,
    Special
}

public class BookUI : BaseMenuContentUI
{
    [SerializeField] Button ingredientBookBtn;
    [SerializeField] Button mixBookBtn;
    [SerializeField] Button specialBookBtn;
    [SerializeField] Button dishBookBtn;

    Image IngredientBookBtnImg;
    Image mixBookBtnImg;
    Image specialBookBtnImg;
    Image dishBookBtnImg;
    Color gray = new Color(0.8f, 0.8f, 0.8f);

    Dictionary<BookType, IBook> books;

    BookType currentBook;
    Image currentBtnImg;

    bool isReady;

    public override void CreateContent()
    {
        if (!isReady)
        {
            ingredientBookBtn.onClick.AddListener(() => OnClickButton(BookType.Ingredient));
            mixBookBtn.onClick.AddListener(() => OnClickButton(BookType.Mix));
            specialBookBtn.onClick.AddListener(() => OnClickButton(BookType.Special));
            dishBookBtn.onClick.AddListener(() => OnClickButton(BookType.Dish));

            IngredientBookBtnImg = ingredientBookBtn.GetComponent<Image>();
            mixBookBtnImg = mixBookBtn.GetComponent<Image>();
            specialBookBtnImg = specialBookBtn.GetComponent<Image>();
            dishBookBtnImg = dishBookBtn.GetComponent<Image>();

            IngredientBookBtnImg.color = Color.white;
            currentBtnImg = IngredientBookBtnImg;
            mixBookBtnImg.color = gray;
            specialBookBtnImg.color = gray;
            dishBookBtnImg.color = gray;

            currentBook = BookType.Ingredient;
            books[currentBook].On();

            isReady = true;
        }
    }

    private void OnClickButton(BookType book)
    {
        if (currentBook == book) return;
        OpenBook(book);
        ColorButton(book);
    }

    private void OpenBook(BookType book)
    {
        books[currentBook].Off();
        books[book].On();
        currentBook = book;
    }

    private void ColorButton(BookType book)
    {
        currentBtnImg.color = gray;
        switch (book)
        {
            case BookType.Ingredient:
                currentBtnImg = IngredientBookBtnImg;
                break;
            case BookType.Mix:
                currentBtnImg = mixBookBtnImg;
                break;
            case BookType.Special:
                currentBtnImg = specialBookBtnImg;
                break;
            case BookType.Dish:
                currentBtnImg = dishBookBtnImg;
                break;
        }
        currentBtnImg.color = Color.white;
    }
}
