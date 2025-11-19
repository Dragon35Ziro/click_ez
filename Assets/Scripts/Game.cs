using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using SQLiter;
using Mono.Data.SqliteClient;
using System.Data;

public class Game : MonoBehaviour
{
    private int num, diamond, control, price, auto, price_auto, click; // присваиваем значение int переменным
    public TextMeshProUGUI score, diamon_text, diamond_text_shop, diamond_tx_aciv, text_up, text_auto, level_auto, level_click, tex_koll, text_achiv_click, text_achiv_LVauto, text_achiv_lvl_click, fon_poluch, fon_im; // присваеваем текстовое значение от unity
    public GameObject main, shop, Aciv, statistica, Vxod, registrac2; // указываем панельки в юнити
                                                          // public Sprite standart; // тут и так понятно
    public GameObject nepoluch, viborFon, vas_ne_zvali, poluchilos1;

    /// <summary>
    /// переменные для работы магазина
    /// </summary>
    public GameObject button_pokupka1, button_vibor1, button_pokupka2, button_vibor2, button_pokupka3, button_vibor3, button_pokupka4, button_vibor4, button_pokupka5, button_vibor5, button_pokupka6, button_vibor6, button_pokupka7, button_vibor7, button_vibor8, button_vibor9, button_pokupka9; // кнопки, чтоб переключать кнопки для скинов
    public TextMeshProUGUI pokupka_text1, button_text1, button_text2, button_text3, button_text4, button_text5, button_text6, button_text7, button_text8, button_text9, sandartfon_text, stanfon_text; // текст

    public GameObject EJ1, EJ2, EJ3, EJ4, EJ5, EJ6, EJ7, EJ8; // переключение скинов

    /// <summary>
    /// переменные для работы очивок
    /// </summary>

    public TextMeshProUGUI clicks_volue, fons; // текст
    public TMP_InputField login, parol; // ввод текста для входа 

    public Sprite fon_classik;
    public Sprite fon_new;
    public Sprite fon3;

    public float timeUser; // переменная для хранения времени
    public static int diamonds; // глобальная переменная алмазиков

    /// <summary>
    /// Переменные для регистрации
    /// </summary>

    public TMP_InputField login_new, parol_new, surname_new, name_new; // переменные для ввода пользователем данных


    private float timeSpent;
    private bool isGameRunning;

    public TextMeshProUGUI timetime; // текст времени


    void Update() // обновление алмазов вместе с чем-то не помню
    {
        diamonds = diamond;



    }

    private void Start()
    {
        click = 1;

        //diamond = diamonds; // одна переменная равна другой
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("num")) // проверяет, существует ли ключ с именем «num» в хранилище данных настроек игрока в игре.
        {
            num = PlayerPrefs.GetInt("num");// сохранение очков
            score.text = num.ToString(); // обновление в текстовом виде очков
            diamond = PlayerPrefs.GetInt("diamond"); // сохранение алмазов
            diamon_text.text = diamond.ToString(); // обновление алмазов
            diamond_text_shop.text = diamond.ToString(); // 
            //click = PlayerPrefs.GetInt("click", click); // сохранение клика
            level_click.text = click.ToString(); // обновление в текстовом виде
            auto = PlayerPrefs.GetInt("auto", auto); // сохранение
            level_auto.text = auto.ToString(); // обновление
            price = PlayerPrefs.GetInt("price", price); // сохранение 
            text_up.text = "улучшить: " + price; //
            price_auto = PlayerPrefs.GetInt("price_auto", price_auto); //
            text_auto.text = "автокликер " + price_auto; //
        }
        else
        {
            click = 1; // присвоение значения переменной для количества нажатий
            price = 1000; // цена прокачки клика
            price_auto = 10000; // цена прокачки авто-клика
            /*PlayerPrefs.SetInt("click", click); // сохранение 
            PlayerPrefs.SetInt("price", price); // сохранение
            PlayerPrefs.SetInt("price_auto", price_auto); // сохранение*/
        }
    }

    private void FixedUpdate() // функция обновления экрана
    {

        num += auto; // прибавление значения автоматического клика
        PlayerPrefs.SetInt("num", num); // сохранение
        score.text = num.ToString(); // обновление
        timeUser += Time.deltaTime; // 
        control += auto; // прибавление авт.клика для добавления алмазиков
        if (control > 10) // условный оператор для получения алмазиков
        {
            diamond += control / 10; // прибавление алмазов
            PlayerPrefs.SetInt("diamond", diamond); // сохранение
            diamon_text.text = diamond.ToString();
            diamond_text_shop.text = diamond.ToString(); // обновление
            control = 0; // обнуление переменной для повторного использования

        }

        

    }

    public string login_user; // переменная для сохранения логина пользователя
    public void ToMain() // функция для перехода на главный экран
    {
        DB db = new DB(); // определение бд

        DataTable table = new DataTable(); // создание таблицы 

        SqliteDataAdapter adapter = new SqliteDataAdapter(); // создание адаптера


        db.openConnection(); // открытие подключения к бд
        SqliteCommand command = new SqliteCommand("SELECT * FROM Account WHERE Passworde = @uP AND Login = @uL;", db.getConnection()); // подключение к бд и передача запроса

        command.Parameters.Add("@uP", SqlDbType.Char).Value = parol.text; // определение маски
        command.Parameters.Add("@uL", SqlDbType.Char).Value = login.text; // тоже самое



        adapter.SelectCommand = command; // выполнение запроса
        adapter.Fill(table); // передача результата запроса в переменную


        if (table.Rows.Count > 0) // проверка на наличие результата
        {
            login_user = login.text; // переменной присваевается значение логина пользователя

            Vxod.SetActive(false); // выключение панели
            main.SetActive(true); // включение панели
            chek_acc(); // вызов функции на проверку данных пользователя 
            poluchilos1.SetActive(false);
        }
        else // else на всякий случай
        {

        }
        db.closeConnection(); // закрытие  подключения к бд




    }

    bool Baobab = true;
    public static int click_level; // создание переменной для уровня клика
    public void chek_acc()
    {
        // Кнопки в магазине ( кроме "Особой")
        DB db = new DB(); // определение бд


        db.openConnection(); // открытие подключения к бд !!! запомнить !!! 

        SqliteCommand command_EJ_pr_2 = new SqliteCommand("SELECT Hedgehog_2 FROM Hedgehog WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @LoginU)", db.getConnection()); // подключение к бд и передача запроса

        command_EJ_pr_2.Parameters.Add("@LoginU", login_user); // определение маски !!! запомнить !!! 

        string EJ_pr_2 = (string)command_EJ_pr_2.ExecuteScalar(); // присвоенние переменной значения наличия ежика !!! запомнить !!! 

        db.closeConnection(); // закрытие  подключения к бд (дальше не буду описывать тоже самое, запоминаем (рассказывать тоже (кто забыл тот не сдал))), p.s Попова Михаила Александровича тоже касается 
        if (EJ_pr_2 == "True") // проверка наличия ежа (Гады, чтоб запомнили)
        {
            button_pokupka2.SetActive(false); // дизактивация кнопки !!! запомнить !!! 
            button_vibor2.SetActive(true); // активация кнопки (нужной) !!! запомнить !!!
        }






        db.openConnection();

        SqliteCommand command_EJ_pr_3 = new SqliteCommand("SELECT Hedgehog_3 FROM Hedgehog WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @LoginU)", db.getConnection());

        command_EJ_pr_3.Parameters.Add("@LoginU", login_user);

        string EJ_pr_3 = (string)command_EJ_pr_3.ExecuteScalar();

        db.closeConnection();
        if (EJ_pr_3 == "True")
        {
            button_pokupka3.SetActive(false);
            button_vibor3.SetActive(true);
        }


        db.openConnection();

        SqliteCommand command_EJ_pr_4 = new SqliteCommand("SELECT Hedgehog_4 FROM Hedgehog WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @LoginU)", db.getConnection());

        command_EJ_pr_4.Parameters.Add("@LoginU", login_user);

        string EJ_pr_4 = (string)command_EJ_pr_4.ExecuteScalar();

        db.closeConnection();
        if (EJ_pr_4 == "True")
        {
            button_pokupka4.SetActive(false);
            button_vibor4.SetActive(true);
        }


        db.openConnection();

        SqliteCommand command_EJ_pr_5 = new SqliteCommand("SELECT Hedgehog_5 FROM Hedgehog WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @LoginU)", db.getConnection());

        command_EJ_pr_5.Parameters.Add("@LoginU", login_user);

        string EJ_pr_5 = (string)command_EJ_pr_5.ExecuteScalar();

        db.closeConnection();
        if (EJ_pr_5 == "True")
        {
            button_pokupka5.SetActive(false);
            button_vibor5.SetActive(true);
        }


        db.openConnection();

        SqliteCommand command_EJ_pr_6 = new SqliteCommand("SELECT Hedgehog_6 FROM Hedgehog WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @LoginU)", db.getConnection());

        command_EJ_pr_6.Parameters.Add("@LoginU", login_user);

        string EJ_pr_6 = (string)command_EJ_pr_6.ExecuteScalar();

        db.closeConnection();
        if (EJ_pr_6 == "True")
        {
            button_pokupka6.SetActive(false);
            button_vibor6.SetActive(true);
        }



        db.openConnection();

        SqliteCommand command_EJ_pr_7 = new SqliteCommand("SELECT Hedgehog_7 FROM Hedgehog WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @LoginU)", db.getConnection());

        command_EJ_pr_7.Parameters.Add("@LoginU", login_user);

        string EJ_pr_7 = (string)command_EJ_pr_7.ExecuteScalar();

        db.closeConnection();
        if (EJ_pr_7 == "True")
        {
            button_pokupka7.SetActive(false);
            button_vibor7.SetActive(true);
        }


        db.openConnection();

        SqliteCommand command_EJ_pr_8 = new SqliteCommand("SELECT Hedgehog_8 FROM Hedgehog WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @LoginU)", db.getConnection());

        command_EJ_pr_8.Parameters.Add("@LoginU", login_user);

        string EJ_pr_8 = (string)command_EJ_pr_8.ExecuteScalar();

        db.closeConnection();
        if (EJ_pr_8 == "True")
        {
            button_pokupka1.SetActive(false);
            button_vibor1.SetActive(true);
        }

        //// Кнопки прокачки в магазине и уровни прокачки кликов

        // Механика обычного клика
        db.openConnection();

        SqliteCommand command_Click_level = new SqliteCommand("SELECT Click_level FROM Statistic WHERE ID_User = @LoginU", db.getConnection());

        command_Click_level.Parameters.Add("@LoginU", login_user);

        int click_level_pol = (int)command_Click_level.ExecuteScalar();


        price = 1000;
        db.closeConnection();
        if (click_level_pol > 0)
        { // проверка уровня пользователя
            click_level = click_level_pol; // присвоение переменной уровня пользователя

            click += click_level * 1; // присвоение значение клика

            price += (click_level * 1000);

            text_up.text = "улучшить: " + price;
            level_click.text = click_level.ToString();
        }
        else
        {
            text_up.text = "улучшить: " + price;
            click = 1;
            click_level = 0;

        }


        // Механика автоклика
        db.openConnection();

        SqliteCommand command_AutoClick_level = new SqliteCommand("SELECT Auto_click_level FROM Statistic WHERE ID_User = @LoginU", db.getConnection());

        command_AutoClick_level.Parameters.Add("@LoginU", login_user);

        int autoclick_level_pol = (int)command_AutoClick_level.ExecuteScalar();

        db.closeConnection();

        auto = autoclick_level_pol;


        price_auto = 10000;
        price_auto = price_auto + (autoclick_level_pol * 10);
        text_auto.text = "автокликер " + price_auto;
        level_auto.text = auto.ToString();


        //// Фон за монетки ( 3 фон )



        db.openConnection();

        SqliteCommand commandFon = new SqliteCommand("SELECT Background_3 FROM Background WHERE ID_Background = (SELECT Background FROM Inventory WHERE ID_User = @LoginU)", db.getConnection());

        commandFon.Parameters.Add("@LoginU", login_user);

        string fon_pr = (string)commandFon.ExecuteScalar();

        db.closeConnection();
        if (fon_pr == "True")
        {
            button_pokupka9.SetActive(false);
            button_vibor9.SetActive(true);
            button_text9.text = "Установить фон";
        }


        //// Статистика
        db.openConnection();

        SqliteCommand command_Click_num = new SqliteCommand("SELECT Number_of_click FROM Statistic WHERE ID_User = @LoginU", db.getConnection());

        command_Click_num.Parameters.Add("@LoginU", login_user);

        int click_num_pol = (int)command_Click_num.ExecuteScalar();

        db.closeConnection();

        string cl_num_polString = click_num_pol.ToString();

        tex_koll.text = cl_num_polString;
        Statistik.kol = click_num_pol;



        db.openConnection();

        SqliteCommand command_time_pol = new SqliteCommand("SELECT Time_in_game FROM Statistic WHERE ID_User = @LoginU", db.getConnection());

        command_time_pol.Parameters.Add("@LoginU", login_user);

        int time_pol = (int)command_time_pol.ExecuteScalar();

        db.closeConnection();

        Timer.timeSpent = (float)time_pol;

        //Валюта и счётчик


        db.openConnection();

        SqliteCommand command_num_cl = new SqliteCommand("SELECT Clicl_num FROM Account WHERE Login = @LoginU", db.getConnection());

        command_num_cl.Parameters.Add("@LoginU", login_user);

        int clicks = (int)command_num_cl.ExecuteScalar();

        db.closeConnection();

        num = clicks;


        db.openConnection();

        SqliteCommand command_num_dm = new SqliteCommand("SELECT Diamond_num FROM Account WHERE Login = @LoginU", db.getConnection());

        command_num_dm.Parameters.Add("@LoginU", login_user);

        int diamonds = (int)command_num_dm.ExecuteScalar();

        db.closeConnection();

        diamond = diamonds;

        diamon_text.text = diamond.ToString();
        diamond_text_shop.text = diamond.ToString();

        if (Baobab)
        {
            control = num % 10;
            Baobab = false;

        }

        //Левелы прокачки кликов прописаны ранее





        //// Ачивки

        db.openConnection();

        SqliteCommand commandachiv1 = new SqliteCommand("SELECT Achivement_1 FROM Achivements WHERE ID_User = @LoginU", db.getConnection());

        commandachiv1.Parameters.Add("@LoginU", login_user);

        string achiv1_pr = (string)commandachiv1.ExecuteScalar();

        db.closeConnection();
        if (achiv1_pr == "True")
        {
            //N_click();
            text_achiv_click.text = "Получено";
        }


        db.openConnection();

        SqliteCommand commandachiv3 = new SqliteCommand("SELECT Achivement_3 FROM Achivements WHERE ID_User = @LoginU", db.getConnection());

        commandachiv3.Parameters.Add("@LoginU", login_user);

        string achiv3_pr = (string)commandachiv3.ExecuteScalar();

        db.closeConnection();
        if (achiv3_pr == "True")
        {
            //N_Auto();
            text_achiv_LVauto.text = "Получено";

        }




        db.openConnection();

        SqliteCommand commandachiv2 = new SqliteCommand("SELECT Achivement_2 FROM Achivements WHERE ID_User = @LoginU", db.getConnection());

        commandachiv2.Parameters.Add("@LoginU", login_user);

        string achiv2_pr = (string)commandachiv2.ExecuteScalar();

        db.closeConnection();
        if (achiv2_pr == "True")
        {
            //N_level();
            text_achiv_lvl_click.text = "Получено";
            button_pokupka1.SetActive(false);
            button_vibor1.SetActive(true);
            button_text1.text = "Установить скин";
        }




        db.openConnection();
        SqliteCommand commandachiv4 = new SqliteCommand("SELECT Achivement_4 FROM Achivements WHERE ID_User = @LoginU", db.getConnection());

        commandachiv4.Parameters.Add("@LoginU", login_user);

        string achiv4_pr = (string)commandachiv4.ExecuteScalar();

        db.closeConnection();

        if (achiv4_pr == "True")
        {
            //int Time = Convert.ToInt32(Timer.timeSpent);
            fon_im.text = "Установить";
            fons.text = "Получено";
            nepoluch.SetActive(false);
            viborFon.SetActive(true);

        }
        //// Сохранение фонов и скинов
        db.openConnection();

        SqliteCommand commandustskin = new SqliteCommand("SELECT Nomer_ej FROM Hedgehog WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @LoginU)", db.getConnection());

        commandustskin.Parameters.Add("@LoginU", login_user);

        int ustskin = (int)commandustskin.ExecuteScalar();

        db.closeConnection();
        if (ustskin == 8)
        {

            button_text1.text = "Убрать скин";

            EJ2.SetActive(true);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);


            button_text2.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text8.text = "Установить скин";

            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;
        }
        else if (ustskin == 2)
        {
            button_text2.text = "Убрать скин";

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(true);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text1.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text8.text = "Установить скин";

            click_count = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;
        }
        else if (ustskin == 3)
        {

            button_text3.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(true);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text2.text = "Установить скин";
            button_text1.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text8.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;
        }
        else if (ustskin == 4)
        {

            button_text4.text = "Убрать скин";

            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(true);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text8.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;
        }
        else if (ustskin == 5)
        {

            button_text5.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(true);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text8.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;
        }
        else if (ustskin == 6)
        {

            button_text6.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(true);
            EJ8.SetActive(false);

            button_text8.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count7 = 0;
            click_count8 = 0;
        }
        else if (ustskin == 7)
        {

            button_text7.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(true);

            button_text8.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count8 = 0;
        }
        else if (ustskin == 1)
        {

            button_text8.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text7.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;
        }



        db.openConnection();

        SqliteCommand commandustfon = new SqliteCommand("SELECT ID_fona FROM Background WHERE ID_Background = (SELECT Background FROM Inventory WHERE ID_User = @LoginU)", db.getConnection());

        commandustfon.Parameters.Add("@LoginU", login_user);

        int ustfon = (int)commandustfon.ExecuteScalar();

        db.closeConnection();

        if (ustfon == 2)
        {
            clikcc_fons();
        }
        else if (ustfon == 3)
        {
            vibor_fon_achiv();
        }
        else
        {
            Fon_standart();
        }







    }



    public void ClickButton()
    {
        num += click;
        control += click;


        if (control == 10)
        {
            diamond += control / 10;
            diamon_text.text = diamond.ToString();
            control = 0;
        }
        score.text = num.ToString();
    }

    public void ToShop()
    {
        main.SetActive(false);
        // Aciv.SetActive(false);
        shop.SetActive(true);
        diamond_text_shop.text = diamond.ToString();
    }

    public void ToAciv()
    {
        main.SetActive(false);
        // shop.SetActive(false);
        Aciv.SetActive(true);
        diamond_tx_aciv.text = diamond.ToString();
    }

    public void Tostatistica()
    {
        main.SetActive(false);
        statistica.SetActive(true);
    }

    public void Exit()
    {
        main.SetActive(true);
        shop.SetActive(false);
        Aciv.SetActive(false);
        statistica.SetActive(false);
        diamond_text_shop.text = diamond.ToString();
    }

    //прокачка клика
    public void Upbutton()
    {
        if (diamond > price)
        {
            click += 1;
            PlayerPrefs.SetInt("click", click);
            diamond -= price;
            diamond_text_shop.text = diamond.ToString();
            price += 1000;
            PlayerPrefs.SetInt("price", price);
            text_up.text = "улучшить: " + price;
            click_level += 1;
            level_click.text = click_level.ToString();

            // click_level - уровень клика (Сохраняется в бд)
            // price - цена
            // click - коэффецент клика
        }
    }
    // прокачка автоклика
    public void Auto()
    {
        if (diamond > price_auto)
        {
            auto++;
            PlayerPrefs.SetInt("auto", auto); // сохранение
            diamond -= price_auto;
            diamond_text_shop.text = diamond.ToString();
            price_auto += 10000;
            PlayerPrefs.SetInt("price_auto", price_auto);
            text_auto.text = "автокликер " + price_auto;
            level_auto.text = auto.ToString();
            // auto - уровень АВТОклика и сам автоклик (Сохраняется в бд) - коэффецент 
            // price_auto - цена 
            

        }
    }


    //
    //  Дальше идёт код магазина скинов
    // 
  

    public void shop_skin2()
    {


        if (diamond >= 4000)
        {

            diamond -= 4000;
            diamond_text_shop.text = diamond.ToString();
            button_pokupka2.SetActive(false);
            button_vibor2.SetActive(true);
            button_text2.text = "Установить скин";
            click_pk3 = 1;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandEJ1 = new SqliteCommand("UPDATE Hedgehog SET Hedgehog_2 = 'True' WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lu)", db.getConnection());

            commandEJ1.Parameters.Add("@Lu", login_user);

            var gg = commandEJ1.ExecuteNonQuery();

            db.closeConnection();


            db.openConnection();


            SqliteCommand commandINvent = new SqliteCommand("SELECT Number_of_skins FROM Inventory WHERE ID_User = @INv", db.getConnection());

            commandINvent.Parameters.Add("@INv", login_user);

            int Invento = (int)commandINvent.ExecuteScalar();

            db.closeConnection();

            Invento++;

            


            db.openConnection();


            SqliteCommand commandINvent1 = new SqliteCommand("UPDATE Inventory SET Number_of_skins = @sk WHERE ID_User = @INvn", db.getConnection());

            commandINvent1.Parameters.Add("@INvn", login_user);
            commandINvent1.Parameters.Add("@sk", Invento);

            var inn = commandINvent1.ExecuteNonQuery();



            db.closeConnection();

        }


    }

    public void shop_skin3()
    {


        if (diamond >= 500)
        {

            diamond -= 500;
            diamond_text_shop.text = diamond.ToString();
            button_pokupka3.SetActive(false);
            button_vibor3.SetActive(true);
            button_text3.text = "Установить скин";
            click_pk4 = 1;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandEJ2 = new SqliteCommand("UPDATE Hedgehog SET Hedgehog_3 = 'True' WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lu)", db.getConnection());

            commandEJ2.Parameters.Add("@Lu", login_user);

            var gg1 = commandEJ2.ExecuteNonQuery();

            db.closeConnection();



            db.openConnection();


            SqliteCommand commandINvent3 = new SqliteCommand("SELECT Number_of_skins FROM Inventory WHERE ID_User = @INvnt", db.getConnection());

            commandINvent3.Parameters.Add("@INvnt", login_user);

            int Invento = (int)commandINvent3.ExecuteScalar();

            db.closeConnection();

            Invento++;




            db.openConnection();


            SqliteCommand commandINvent4 = new SqliteCommand("UPDATE Inventory SET Number_of_skins = @skk WHERE ID_User = @INvntt", db.getConnection());

            commandINvent4.Parameters.Add("@INvntt", login_user);
            commandINvent4.Parameters.Add("@skk", Invento);

            var inn = commandINvent4.ExecuteNonQuery();



            db.closeConnection();
        }
    }

    public void shop_skin4()
    {


        if (diamond >= 1000)
        {

            diamond -= 1000;
            diamond_text_shop.text = diamond.ToString();
            button_pokupka4.SetActive(false);
            button_vibor4.SetActive(true);
            button_text4.text = "Установить скин";
            click_pk5 = 1;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandEJ3 = new SqliteCommand("UPDATE Hedgehog SET Hedgehog_4 = 'True' WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lu)", db.getConnection());

            commandEJ3.Parameters.Add("@Lu", login_user);

            var gg2 = commandEJ3.ExecuteNonQuery();

            db.closeConnection();





            db.openConnection();


            SqliteCommand commandINvent5 = new SqliteCommand("SELECT Number_of_skins FROM Inventory WHERE ID_User = @INvvv", db.getConnection());

            commandINvent5.Parameters.Add("@INvvv", login_user);

            int Invento = (int)commandINvent5.ExecuteScalar();

            db.closeConnection();

            Invento++;




            db.openConnection();


            SqliteCommand commandINvent6 = new SqliteCommand("UPDATE Inventory SET Number_of_skins = @sksk WHERE ID_User = @INvnvn", db.getConnection());

            commandINvent6.Parameters.Add("@INvnvn", login_user);
            commandINvent6.Parameters.Add("@sksk", Invento);

            var inn = commandINvent6.ExecuteNonQuery();



            db.closeConnection();
        }
    }

    public void shop_skin5()
    {


        if (diamond >= 2000)
        {

            diamond -= 2000;
            diamond_text_shop.text = diamond.ToString();
            button_pokupka5.SetActive(false);
            button_vibor5.SetActive(true);
            button_text5.text = "Установить скин";
            click_pk6 = 1;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandEJ4 = new SqliteCommand("UPDATE Hedgehog SET Hedgehog_5 = 'True' WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lu)", db.getConnection());

            commandEJ4.Parameters.Add("@Lu", login_user);

            var gg3 = commandEJ4.ExecuteNonQuery();

            db.closeConnection();




            db.openConnection();


            SqliteCommand commandINvent7 = new SqliteCommand("SELECT Number_of_skins FROM Inventory WHERE ID_User = @INv1", db.getConnection());

            commandINvent7.Parameters.Add("@INv1", login_user);

            int Invento = (int)commandINvent7.ExecuteScalar();

            db.closeConnection();

            Invento++;




            db.openConnection();


            SqliteCommand commandINvent8 = new SqliteCommand("UPDATE Inventory SET Number_of_skins = @sk1 WHERE ID_User = @INvn1", db.getConnection());

            commandINvent8.Parameters.Add("@INvn1", login_user);
            commandINvent8.Parameters.Add("@sk1", Invento);

            var inn = commandINvent8.ExecuteNonQuery();



            db.closeConnection();
        }
    }

    public void shop_skin6()
    {


        if (diamond >= 16000)
        {

            diamond -= 16000;
            diamond_text_shop.text = diamond.ToString();
            button_pokupka6.SetActive(false);
            button_vibor6.SetActive(true);
            button_text6.text = "Установить скин";
            click_pk7 = 1;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandEJ5 = new SqliteCommand("UPDATE Hedgehog SET Hedgehog_6 = 'True' WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lu)", db.getConnection());

            commandEJ5.Parameters.Add("@Lu", login_user);

            var gg4 = commandEJ5.ExecuteNonQuery();

            db.closeConnection();




            db.openConnection();


            SqliteCommand commandINventa = new SqliteCommand("SELECT Number_of_skins FROM Inventory WHERE ID_User = @INv2", db.getConnection());

            commandINventa.Parameters.Add("@INv2", login_user);

            int Invento = (int)commandINventa.ExecuteScalar();

            db.closeConnection();

            Invento++;




            db.openConnection();


            SqliteCommand commandINventa1 = new SqliteCommand("UPDATE Inventory SET Number_of_skins = @sk2 WHERE ID_User = @INvn2", db.getConnection());

            commandINventa1.Parameters.Add("@INvn2", login_user);
            commandINventa1.Parameters.Add("@sk2", Invento);

            var inn = commandINventa1.ExecuteNonQuery();



            db.closeConnection();
        }
    }

    public void shop_skin7()
    {


        if (diamond >= 8000)
        {

            diamond -= 8000;
            diamond_text_shop.text = diamond.ToString();
            button_pokupka7.SetActive(false);
            button_vibor7.SetActive(true);
            button_text7.text = "Установить скин";
            click_pk8 = 1;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandEJ6 = new SqliteCommand("UPDATE Hedgehog SET Hedgehog_7 = 'True' WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lu)", db.getConnection());

            commandEJ6.Parameters.Add("@Lu", login_user);

            var gg5 = commandEJ6.ExecuteNonQuery();

            db.closeConnection();




            db.openConnection();


            SqliteCommand commandINventa2 = new SqliteCommand("SELECT Number_of_skins FROM Inventory WHERE ID_User = @InVe", db.getConnection());

            commandINventa2.Parameters.Add("@InVe", login_user);

            int Invento = (int)commandINventa2.ExecuteScalar();

            db.closeConnection();

            Invento++;




            db.openConnection();


            SqliteCommand commandINventa3 = new SqliteCommand("UPDATE Inventory SET Number_of_skins = @skK1 WHERE ID_User = @INvenT", db.getConnection());

            commandINventa3.Parameters.Add("@INvenT", login_user);
            commandINventa3.Parameters.Add("@skK1", Invento);

            var inn = commandINventa3.ExecuteNonQuery();



            db.closeConnection();
        }
    }

    public int click_fon = 0;

    public void shop_fon3()
    {


        if (diamond >= 5000)
        {

            diamond -= 5000;
            diamond_text_shop.text = diamond.ToString();
            button_pokupka9.SetActive(false);
            button_vibor9.SetActive(true);
            button_text9.text = "Установить фон";
            click_pk9 = 1;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandFon = new SqliteCommand("UPDATE Background SET Background_3 = 'True' WHERE ID_Background = (SELECT Background FROM Inventory WHERE ID_User = @Fo)", db.getConnection());

            commandFon.Parameters.Add("@Fo", login_user);

            var ff = commandFon.ExecuteNonQuery();

            db.closeConnection();





            db.openConnection();


            SqliteCommand commandFonts = new SqliteCommand("SELECT Number_of_background FROM Inventory WHERE ID_User = @Fon1", db.getConnection());

            commandFonts.Parameters.Add("@Fon1", login_user);

            int Invento = (int)commandFonts.ExecuteScalar();

            db.closeConnection();

            Invento++;




            db.openConnection();


            SqliteCommand commandFonts2 = new SqliteCommand("UPDATE Inventory SET Number_of_background = @Fon3 WHERE ID_User = @Don1", db.getConnection());

            commandFonts2.Parameters.Add("@Don1", login_user);
            commandFonts2.Parameters.Add("@Fon3", Invento);

            var inn = commandFonts2.ExecuteNonQuery();

            

            db.closeConnection();
        }
    }

    public void clikcc_fons()
    {
        if (click_fon == 0)
        {
            main.GetComponent<Image>().sprite = fon3;
            button_text9.text = "Установлен";
            sandartfon_text.text = "Установить фон";
            fon_im.text = "Установить фон";


            DB db = new DB();
            DataTable table = new DataTable();

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();

            SqliteCommand commandFons = new SqliteCommand("UPDATE Background SET ID_fona = 2 WHERE ID_Background = (SELECT Background FROM Inventory WHERE ID_User = @Lupa4)", db.getConnection());

            commandFons.Parameters.Add("@Lupa4", login_user);

            var kol1 = commandFons.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_fon == 1)
        {
            main.GetComponent<Image>().sprite = fon_classik;
            button_text9.text = "Установить фон";
            sandartfon_text.text = "Установлен";
            fon_im.text = "Установить фон";
            

        }
        click_fon++;

        if (click_fon == 2) click_fon = 0;
    }


    public int click_count;
    public int click_count2;
    public int click_count3;
    public int click_count4;
    public int click_count5;
    public int click_count6;
    public int click_count7;
    public int click_count8;

    public int click_pk1 = 1;
    public int click_pk2;
    public int click_pk3;
    public int click_pk4;
    public int click_pk5;
    public int click_pk6;
    public int click_pk7;
    public int click_pk8;
    public int click_pk9;

    public void vibor_skin()
    {
        if (click_count == 0)
        {
            button_text1.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(true);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text2.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text8.text = "Установить скин";

            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;


            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandNEJ = new SqliteCommand("UPDATE Hedgehog SET Nomer_ej = 1 WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lupapa)", db.getConnection());

            commandNEJ.Parameters.Add("@Lupapa", login_user);

            var kl = commandNEJ.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_count == 1)
        {
            button_text1.text = "Установить скин";
            button_text8.text = "Скин выбран";
            //EJ.GetComponent<Image>().color = Color.white;
            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

        }

        click_count++;
        if (click_count == 2)
        {
            click_count = 0;
        }


    }

    public void vibor_skin2()
    {
        if (click_count2 == 0)
        {
            button_text2.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(true);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text1.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text8.text = "Установить скин";

            click_count = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;


            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandNEJ1 = new SqliteCommand("UPDATE Hedgehog SET Nomer_ej = 2 WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lupapa1)", db.getConnection());

            commandNEJ1.Parameters.Add("@Lupapa1", login_user);

            var kll1 = commandNEJ1.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_count2 == 1)
        {
            button_text2.text = "Установить скин";
            button_text8.text = "Скин выбран";
            //EJ.GetComponent<Image>().color = Color.white;
            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

        }

        click_count2++;
        if (click_count2 == 2)
        {
            click_count2 = 0;
        }






    }




    public void vibor_skin3()
    {
        if (click_count3 == 0)
        {
            button_text3.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(true);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text2.text = "Установить скин";
            button_text1.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text8.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;


            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandNEJ2 = new SqliteCommand("UPDATE Hedgehog SET Nomer_ej = 3 WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lupapa2)", db.getConnection());

            commandNEJ2.Parameters.Add("@Lupapa2", login_user);

            var kl2 = commandNEJ2.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_count3 == 1)
        {
            button_text3.text = "Установить скин";
            button_text8.text = "Скин выбран";
            //EJ.GetComponent<Image>().color = Color.white;
            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

        }

        click_count3++;
        if (click_count3 == 2)
        {
            click_count3 = 0;
        }


    }

    public void vibor_skin4()
    {
        if (click_count4 == 0)
        {
            button_text4.text = "Убрать скин";
            
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(true);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text8.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;


            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandNEJ3 = new SqliteCommand("UPDATE Hedgehog SET Nomer_ej = 4 WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lupapa3)", db.getConnection());

            commandNEJ3.Parameters.Add("@Lupapa3", login_user);

            var kl3 = commandNEJ3.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_count4 == 1)
        {
            button_text4.text = "Установить скин";
            button_text8.text = "Скин выбран";
            //EJ.GetComponent<Image>().color = Color.white;

            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

        }

        click_count4++;
        
        if (click_count4 == 2)
        {
            click_count4 = 0;
        }



    }

    public void vibor_skin5()
    {
        if (click_count5 == 0)
        {
            button_text5.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(true);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text8.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count6 = 0;
            click_count7 = 0;
            click_count8 = 0;


            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandNEJ4 = new SqliteCommand("UPDATE Hedgehog SET Nomer_ej = 5 WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lupapa4)", db.getConnection());

            commandNEJ4.Parameters.Add("@Lupapa4", login_user);

            var kl4 = commandNEJ4.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_count5 == 1)
        {
            button_text5.text = "Установить скин";
            button_text8.text = "Скин выбрать";
            //EJ.GetComponent<Image>().color = Color.white;
            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

        }

        click_count5++;

        if (click_count5 == 2)
        {
            click_count5 = 0;
        }



    }

    public void vibor_skin6()
    {
        if (click_count6 == 0)
        {
            button_text6.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(true);
            EJ8.SetActive(false);

            button_text8.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text7.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count7 = 0;
            click_count8 = 0;


            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandNEJ5 = new SqliteCommand("UPDATE Hedgehog SET Nomer_ej = 6 WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lupapa5)", db.getConnection());

            commandNEJ5.Parameters.Add("@Lupapa5", login_user);

            var kl5 = commandNEJ5.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_count6 == 1)
        {
            button_text6.text = "Установить скин";
            button_text8.text = "Скин выбрать";
            //EJ.GetComponent<Image>().color = Color.white;
            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

        }

        click_count6++;

        if (click_count6 == 2)
        {
            click_count6 = 0;
        }



    }


    public void vibor_skin7()
    {
        if (click_count7 == 0)
        {
            button_text7.text = "Убрать скин";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(false);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(true);

            button_text8.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count8 = 0;


            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandNEJ6 = new SqliteCommand("UPDATE Hedgehog SET Nomer_ej = 7 WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lupapa6)", db.getConnection());

            commandNEJ6.Parameters.Add("@Lupapa6", login_user);

            var kl6 = commandNEJ6.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_count7 == 1)
        {
            button_text7.text = "Установить скин";
            button_text8.text = "Скин выбран";
            //EJ.GetComponent<Image>().color = Color.white;
            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

        }

        click_count7++;

        if (click_count7 == 2)
        {
            click_count7 = 0;
        }



    }

    public void vibor_skin8()
    {
        if (click_count8 == 0)
        {
            button_text8.text = "Скин выбран";
            //EJ.GetComponent<Image>().color = Color.blue;

            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

            button_text7.text = "Установить скин";
            button_text3.text = "Установить скин";
            button_text2.text = "Установить скин";
            button_text4.text = "Установить скин";
            button_text5.text = "Установить скин";
            button_text6.text = "Установить скин";
            button_text1.text = "Установить скин";

            click_count = 0;
            click_count2 = 0;
            click_count3 = 0;
            click_count4 = 0;
            click_count5 = 0;
            click_count6 = 0;
            click_count7 = 0;


            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandNEJ7 = new SqliteCommand("UPDATE Hedgehog SET Nomer_ej = 8 WHERE ID_Hedgehog = (SELECT Hedgehog_collections FROM Inventory WHERE ID_User = @Lupapa7)", db.getConnection());

            commandNEJ7.Parameters.Add("@Lupapa7", login_user);

            var kl7 = commandNEJ7.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_count8 == 1)
        {
            
            //EJ.GetComponent<Image>().color = Color.white;
            EJ2.SetActive(false);
            EJ1.SetActive(true);
            EJ3.SetActive(false);
            EJ4.SetActive(false);
            EJ5.SetActive(false);
            EJ6.SetActive(false);
            EJ7.SetActive(false);
            EJ8.SetActive(false);

        }


        click_count8++;

        if (click_count8 == 2)
        {
            click_count8 = 0;
        }



    }





    // Дальше идёт код достижений
    public int n_clicks = 1000;
    private string cliki;

    public int stipuha = 1333;
    
    
    

    public void N_click() {


        if (n_clicks <= num)
        {
            text_achiv_click.text = "Получено";
            diamond += stipuha;
            diamon_text.text = diamond.ToString();
            diamond_text_shop.text = diamond.ToString();

            n_clicks = 1000;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandAC = new SqliteCommand("UPDATE Achivements SET Achivement_1 = 'True' WHERE ID_User = @Lu", db.getConnection());

            commandAC.Parameters.Add("@Lu", login_user);

            var acv = commandAC.ExecuteNonQuery();

            db.closeConnection();
        }
    }

    public int click_level_N = 30;

    
    public void N_level()
    {
        if (click_level >= click_level_N)
        {
            text_achiv_lvl_click.text = "Получено";
            // Ежа не будя, он принял ислам
            button_pokupka1.SetActive(false);
            button_vibor1.SetActive(true);
            button_text1.text = "Установить скин";
            click_pk2 = 1;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandAC = new SqliteCommand("UPDATE Achivements SET Achivement_2 = 'True' WHERE ID_User = @Lu", db.getConnection());

            commandAC.Parameters.Add("@Lu", login_user);

            var acv1 = commandAC.ExecuteNonQuery();

            db.closeConnection();



            db.openConnection();


            SqliteCommand commandAcSk = new SqliteCommand("SELECT Number_of_skins FROM Inventory WHERE ID_User = @Ski1", db.getConnection());

            commandAcSk.Parameters.Add("@Ski1", login_user);

            int Invento = (int)commandAcSk.ExecuteScalar();

            db.closeConnection();

            Invento++;




            db.openConnection();


            SqliteCommand commandAcSk1 = new SqliteCommand("UPDATE Inventory SET Number_of_skins = @skii WHERE ID_User = @Lj", db.getConnection());

            commandAcSk1.Parameters.Add("@Lj", login_user);
            commandAcSk1.Parameters.Add("@skii", Invento);

            var inn = commandAcSk1.ExecuteNonQuery();



            db.closeConnection();

        }
    }

    public float click_time_N = 30;

    public int click_count_N;

    public int NT = 1;
    public int gg = 0;


    public void vibor_fon_achiv() 
    {
        if (click_count_N == 0)
        {
            click_fon = 0;
            
            main.GetComponent<Image>().sprite = fon_new;
            fon_im.text = "Установлен";
            sandartfon_text.text = "Установить фон";
            button_text9.text = "Установить фон";


            DB db = new DB();
            DataTable table = new DataTable();

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();

            SqliteCommand commandFons = new SqliteCommand("UPDATE Background SET ID_fona = 3 WHERE ID_Background = (SELECT Background FROM Inventory WHERE ID_User = @Lupa1)", db.getConnection());

            commandFons.Parameters.Add("@Lupa1", login_user);

            var kol = commandFons.ExecuteNonQuery();

            db.closeConnection();


        }
        else if (click_count_N == 1)
        {

            click_fon = 0;
            main.GetComponent<Image>().sprite = fon_classik;
             fon_im.text = "Установить фон";
             sandartfon_text.text = "Установлен";
             button_text9.text = "Установить фон";


        }
         click_count_N++;

        if (click_count_N == 2) click_count_N = 0;



    }
    public int click_count_Nm;

    public void Fon_standart()
    {


        main.GetComponent<Image>().sprite = fon_classik;
        click_count_N = 0;
        click_fon = 0;
        sandartfon_text.text = "Установлен";
        fon_im.text = "Установить фон";
        button_text9.text = "Установить фон";

        // stanfon_text.text = "установить";




    }



    public void N_time()
    {
        int Time = Convert.ToInt32(Timer.timeSpent);
        if (Time >= click_time_N) 
        {
            

            //if (click_count_N == 0)
            //{
                //main.GetComponent<Image>().sprite = fon_new;
            fons.text = "Получено";
            nepoluch.SetActive(false);
            viborFon.SetActive(true);
            fon_im.text = "Выбрать"; 

            // }
            // else if (click_count_N == 1)
            /* {
                 main.GetComponent<Image>().sprite = fon_classik;
                 fons.text = "Установить особый фон";
             }
             click_count_N++;*/

            /*if (click_count_N == 2) click_count_N = 0;*/

            if (gg == 0)
            {
                DB db = new DB();
                DataTable table = new DataTable();

                //bool auth = false;

                SqliteDataAdapter adapter = new SqliteDataAdapter();

                db.openConnection();


                SqliteCommand commandFon = new SqliteCommand("UPDATE Background SET Background_2 = 'True' WHERE ID_Background = (SELECT Background FROM Inventory WHERE ID_User = @Fo)", db.getConnection());

                commandFon.Parameters.Add("@Fo", login_user);

                var ff = commandFon.ExecuteNonQuery();

                db.closeConnection();

                gg = 1;

               

                db.openConnection();


                SqliteCommand commandAC = new SqliteCommand("UPDATE Achivements SET Achivement_4 = 'True' WHERE ID_User = @Lu", db.getConnection());

                commandAC.Parameters.Add("@Lu", login_user);

                var acv4 = commandAC.ExecuteNonQuery();

                db.closeConnection();



                db.openConnection();


                SqliteCommand commandFoNAC = new SqliteCommand("SELECT Number_of_background FROM Inventory WHERE ID_User = @Fof", db.getConnection());

                commandFoNAC.Parameters.Add("@Fof", login_user);

                int Invento = (int)commandFoNAC.ExecuteScalar();

                db.closeConnection();

                Invento++;




                db.openConnection();


                SqliteCommand commandAcFo = new SqliteCommand("UPDATE Inventory SET Number_of_background = @Ffo WHERE ID_User = @Lpo", db.getConnection());

                commandAcFo.Parameters.Add("@Lpo", login_user);
                commandAcFo.Parameters.Add("@Ffo", Invento);

                var inn = commandAcFo.ExecuteNonQuery();



                db.closeConnection();


            }
           

        }
    }


    public int auto_N = 50;
    public int zp = 1886;
    

    public void N_Auto()
    {
        if (auto >= auto_N)
        {
            text_achiv_LVauto.text = "Получено";
            diamond += zp;
            diamon_text.text = diamond.ToString();
            diamond_text_shop.text = diamond.ToString();
            // добавить текст о получении
            auto_N = 50;

            DB db = new DB();
            DataTable table = new DataTable();

            //bool auth = false;

            SqliteDataAdapter adapter = new SqliteDataAdapter();

            db.openConnection();


            SqliteCommand commandAC = new SqliteCommand("UPDATE Achivements SET Achivement_3 = 'True' WHERE ID_User = @Lu", db.getConnection());

            commandAC.Parameters.Add("@Lu", login_user);

            var acv2 = commandAC.ExecuteNonQuery();

            db.closeConnection();
        }
    }




    /// регистрация 
    /// 

    

    public void registr()
    {
        DB db = new DB();
        DataTable table = new DataTable();
        
        //bool auth = false;

        SqliteDataAdapter adapter = new SqliteDataAdapter();

        db.openConnection();


        SqliteCommand commandTest = new SqliteCommand("SELECT * FROM Account WHERE Login = @uL", db.getConnection());
        commandTest.Parameters.Add("@uL", SqlDbType.Char).Value = login_new.text;
        
        adapter.SelectCommand = commandTest;

        //int rowsAffected = commandTest.ExecuteNonQuery();
        
        adapter.Fill(table);

        if (table.Rows.Count > 0)
        {
            vas_ne_zvali.SetActive(true);
            Console.WriteLine("Пользователь с таким логином уже сущесвует, введите другой логин");
        }




        else
        {
            db.closeConnection();

            db.openConnection();



            SqliteCommand commandReg = new SqliteCommand("INSERT INTO Account(Login, Passworde, User_surname, User_name) VALUES(@login, @pass, @name, @surname)", db.getConnection());



            commandReg.Parameters.Add("@login", SqlDbType.Char).Value = login_new.text;
            //command.Parameters.Add("@pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = passwordField.Text;
            commandReg.Parameters.Add("@name", SqlDbType.VarChar).Value = name_new.text;
            commandReg.Parameters.Add("@surname", SqlDbType.VarChar).Value = surname_new.text;
            commandReg.Parameters.Add("@pass", SqlDbType.Char).Value = parol_new.text;


            

          



            //int rowsAffected = commandReg.ExecuteNonQuery();

            //Console.WriteLine($"{rowsAffected} запись(и) добавлено(ы).");

            
            if (commandReg.ExecuteNonQuery() == 1)
            {

                Console.WriteLine("Аккаунт был создан");
                
                db.closeConnection();

                db.openConnection();

                SqliteDataAdapter adapter2 = new SqliteDataAdapter();
                SqliteDataAdapter adapter3 = new SqliteDataAdapter();
                SqliteDataAdapter adapter4 = new SqliteDataAdapter();
                SqliteDataAdapter adapter5 = new SqliteDataAdapter();
                SqliteCommand commandAchiv = new SqliteCommand("INSERT INTO Achivements (Achivement_4, Achivement_3, Achivement_2, Achivement_1, ID_User) VALUES('False','False','False','False', @login);", db.getConnection());
               // SqliteDataAdapter adapter2 = new SqliteDataAdapter();

                commandAchiv.Parameters.Add("@login", SqlDbType.Char).Value = login_new.text;
        
                commandAchiv.ExecuteNonQuery();
                


                db.closeConnection();

                db.openConnection();

                SqliteCommand commandEJ = new SqliteCommand("INSERT INTO Hedgehog(Hedgehog_8, Hedgehog_7, Hedgehog_6, Hedgehog_5, Hedgehog_4, Hedgehog_3, Hedgehog_2, Hedgehog_1, Nomer_ej) VALUES('False', 'False', 'False', 'False', 'False', 'False', 'False', 'True', 1); ", db.getConnection());
                commandEJ.ExecuteNonQuery();

                db.closeConnection();
                
                db.openConnection();
                SqliteCommand commandFons = new SqliteCommand("INSERT INTO Background ( Background_3, Background_2, Background_1, ID_fona) VALUES ('False', 'False', 'True', 1); ", db.getConnection());
                commandFons.ExecuteNonQuery();
                db.closeConnection();


                db.openConnection();
                SqliteCommand commandInventory = new SqliteCommand("INSERT INTO Inventory (ID_User, Number_of_skins, Number_of_background, Background, Hedgehog_collections) VALUES (@login, 1, 1, (SELECT ID_Background FROM Background ORDER BY ID_Background DESC LIMIT 1), (SELECT ID_Hedgehog FROM Hedgehog ORDER BY ID_Hedgehog DESC LIMIT 1));", db.getConnection());
                commandInventory.Parameters.Add("@login", SqlDbType.Char).Value = login_new.text;
                commandInventory.ExecuteNonQuery();
                db.closeConnection();


                db.openConnection();



                SqliteCommand commandRegis = new SqliteCommand("INSERT INTO Statistic(ID_User, Time_in_game, Number_of_click, Click_level, Auto_click_level) VALUES(@inlog, 0, 0, 0, 0)", db.getConnection());
                
                commandRegis.Parameters.Add("@inlog", SqlDbType.Char).Value = login_new.text;
                var vxr = commandRegis.ExecuteNonQuery();



                db.closeConnection();

                registrac2.SetActive(false);
                Vxod.SetActive(true);
                poluchilos1.SetActive(true);


            }
            else 
            { 
                Console.WriteLine("Аккаунт не был создан");
            
            }



            db.closeConnection();

            
            

        }

        

    }

    public void Nas_zvali()
    {
        vas_ne_zvali.SetActive(false);
    }

    public int zaebalo = 0;
    public void Vixod()
    {
        
        DB db = new DB();
        DataTable table = new DataTable();

        //bool auth = false;

        SqliteDataAdapter adapter = new SqliteDataAdapter();
       
        db.openConnection();

        string koll = tex_koll.text;
        int kollik = int.Parse(koll);
        

        SqliteCommand commandVX = new SqliteCommand("UPDATE Statistic SET Number_of_click = @Nun WHERE ID_User = @Lun", db.getConnection());

        commandVX.Parameters.Add("@Nun", kollik);
        commandVX.Parameters.Add("@Lun", login_user);

        var vx = commandVX.ExecuteNonQuery();

        db.closeConnection();

        


        db.openConnection();

        SqliteCommand commandVX1 = new SqliteCommand("UPDATE Statistic SET Click_level = @Nuu WHERE ID_User = @Luu", db.getConnection());

        commandVX1.Parameters.Add("@Nuu", click_level);
        commandVX1.Parameters.Add("@Luu", login_user);

        var vx1 = commandVX1.ExecuteNonQuery();

        db.closeConnection();


        db.openConnection();

        SqliteCommand commandVX2 = new SqliteCommand("UPDATE Statistic SET Auto_click_level = @Nub WHERE ID_User = @Lub", db.getConnection());

        commandVX2.Parameters.Add("@Nub", auto);
        commandVX2.Parameters.Add("@Lub", login_user);

        var vx2 = commandVX2.ExecuteNonQuery();

        db.closeConnection();

        

        db.openConnection();

        SqliteCommand commandVX3 = new SqliteCommand("UPDATE Statistic SET Time_in_game = @Nuv WHERE ID_User = @Luv", db.getConnection());

      

        int time = Convert.ToInt32(Timer.timeSpent);
        commandVX3.Parameters.Add("@Nuv", time);
        commandVX3.Parameters.Add("@Luv", login_user);

        var vx3 = commandVX3.ExecuteNonQuery();

        db.closeConnection();



        db.openConnection();

        SqliteCommand commandVX4 = new SqliteCommand("UPDATE Account SET Diamond_num = @Dn WHERE Login = @Lug", db.getConnection());

        commandVX4.Parameters.Add("@Dn", diamond);
        commandVX4.Parameters.Add("@Lug", login_user);

        var vx4 = commandVX4.ExecuteNonQuery();

        db.closeConnection();



        db.openConnection();

        SqliteCommand commandVX5 = new SqliteCommand("UPDATE Account SET Clicl_num = @Scor WHERE Login = @Lug1", db.getConnection());

        commandVX5.Parameters.Add("@Scor", num);
        commandVX5.Parameters.Add("@Lug1", login_user);

        var vx5 = commandVX5.ExecuteNonQuery();

        db.closeConnection();

        diamond = 0;
        num = 0;
        click_level = 1;
        time = 0;
        auto = 0;
        Statistik.kol = 0;
        level_click.text = "1";
        level_auto.text = "0";

        button_pokupka1.SetActive(true);
        button_vibor1.SetActive(false);

        button_pokupka2.SetActive(true);
        button_vibor2.SetActive(false);

        button_pokupka3.SetActive(true);
        button_vibor3.SetActive(false);

        button_pokupka4.SetActive(true);
        button_vibor4.SetActive(false);

        button_pokupka5.SetActive(true);
        button_vibor5.SetActive(false);

        button_pokupka6.SetActive(true);
        button_vibor6.SetActive(false);

        button_pokupka7.SetActive(true);
        button_vibor7.SetActive(false);

        button_pokupka9.SetActive(true); //фон
        button_vibor9.SetActive(false);

        text_achiv_click.text = "Не получено";

        text_achiv_lvl_click.text = "Не получено";

        text_achiv_LVauto.text = "Не получено";
        
        fons.text = "Не получено";


        price = 50;
        price_auto = 50;
        text_up.text = "улучшить: " + price;
        text_auto.text = "улучшить: " + price_auto;
        




        main.SetActive(false);
        Vxod.SetActive(true);

        
    }



}
