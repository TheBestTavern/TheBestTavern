using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    [SerializeField] Button specialBookBtn;
    [SerializeField] Button mixBookBtn;
    [SerializeField] Button dishBookBtn;

    Image IngredientBookBtnImg;
    Image specialBookBtnImg;
    Image mixBookBtnImg;
    Image dishBookBtnImg;
    Color gray = new Color(0.8f, 0.8f, 0.8f);

    Dictionary<BookType, IBook> books = new();
    BookType currentBook;
    Image currentBtnImg;

    public Action<int> OnClickSlotEvent;

    bool isReady;

    public async override void CreateContent()
    {
        if (!isReady)
        {
            // 버튼
            ingredientBookBtn.onClick.AddListener(() => OnClickButton(BookType.Ingredient));
            specialBookBtn.onClick.AddListener(() => OnClickButton(BookType.Special));
            mixBookBtn.onClick.AddListener(() => OnClickButton(BookType.Mix));
            dishBookBtn.onClick.AddListener(() => OnClickButton(BookType.Dish));

            // 버튼 이미지
            IngredientBookBtnImg = ingredientBookBtn.GetComponent<Image>();
            specialBookBtnImg = specialBookBtn.GetComponent<Image>();
            mixBookBtnImg = mixBookBtn.GetComponent<Image>();
            dishBookBtnImg = dishBookBtn.GetComponent<Image>();
            IngredientBookBtnImg.color = Color.white;
            currentBtnImg = IngredientBookBtnImg;
            specialBookBtnImg.color = gray;
            mixBookBtnImg.color = gray;
            dishBookBtnImg.color = gray;

            // books 초기화
            IBook[] tempbooks = GetComponentsInChildren<IBook>();
            foreach (IBook book in tempbooks)
            {
                book.Init1(this);
                books[book.thisBookType] = book;
                book.Off();
            }
            currentBook = BookType.Ingredient;

            // 첫 화면 열기
            books[currentBook].On();

            // 상세화면 이벤트
            OnClickSlotEvent = async (foodCategoryID) =>
            {
                var detailPopup = (DetailPopup)await PopUpManager.Instance.ShowPopUp(PopUpType.FoodDetail);
                detailPopup.NewDetail(foodCategoryID);
            };

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
            case BookType.Special:
                currentBtnImg = specialBookBtnImg;
                break;
            case BookType.Mix:
                currentBtnImg = mixBookBtnImg;
                break;
            case BookType.Dish:
                currentBtnImg = dishBookBtnImg;
                break;
        }
        currentBtnImg.color = Color.white;
    }

    public void TriggerClickSlotEvent(int foodCategoryID)
    {
        OnClickSlotEvent?.Invoke(foodCategoryID);
    }
}
