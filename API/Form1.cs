using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;


namespace API
{
    public partial class Form1 : Form
    {
        private const string API_KEYweather = "b0836894258ef397df0e344f22193dd0";
        private const string apiKeyMaps = "8c09ec639dea4c8b8a022879a6a436bb";
        string categories = "";
        string numSights = "20";

        public Form1()
        {
            InitializeComponent();
            SetupCityAutocomplete();
        }

        private void SetupCityAutocomplete()
        {
            // Загрузка списка городов из файла
            string[] cities = File.ReadAllLines("city.txt");


            // Создание коллекции для автодополнения
            var autoCompleteCollection = new AutoCompleteStringCollection();
            autoCompleteCollection.AddRange(cities);

            // Применение коллекции к TextBox
            NameCityTxt.AutoCompleteCustomSource = autoCompleteCollection;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            string city = NameCityTxt.Text.Trim();

            if (NameCityTxt.Text.Length == 0) {
                MessageBox.Show("Напишите город");
                return;
            }

            if (checkedListBox1.CheckedIndices.Count == 0) {
                MessageBox.Show("Выберите хотя бы одну категорию");
                return;
            }
            if (numericUpDown1.Value == 0) {
                numericUpDown1.Value = 1;
            }
           
            
            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Введите название города.");
                return;
            }

            try
            {
                var weatherData = await GetWeatherData(city);
                CityTemp.Text = $"Температура: {weatherData["temperature"]} °C";
                cityRain.Text = $"Вероятность осадков: {weatherData["precip"]}%";
                cityWeather.Text = $"Описание погоды: {string.Join(", ", weatherData["weather_descriptions"])}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            

            string lat = "";
            string lon = "";
            string geocodeUrl = $"https://api.geoapify.com/v1/geocode/search?text={city}&format=json&apiKey={apiKeyMaps}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage geocodeResponse = await client.GetAsync(geocodeUrl);
                    if (!geocodeResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Ошибка HTTP: {geocodeResponse.StatusCode}");
                        return;
                    }

                    string geocodeResponseBody = await geocodeResponse.Content.ReadAsStringAsync();
                    JObject geocodeJson = JObject.Parse(geocodeResponseBody);

                    var firstResult = geocodeJson["results"]?[0];

                    if (firstResult != null)
                    {
                        lat = firstResult["lat"]?.Value<string>() ?? "";
                        lon = firstResult["lon"]?.Value<string>() ?? "";

                        //richTextBox1.Text = ($"Координаты города:");
                        //richTextBox1.Text += ($"Широта (lat): {lat}");
                        //richTextBox1.Text += ($"Долгота (lon): {lon}");
                    }
                    else
                    {
                        MessageBox.Show("Результаты не найдены.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при получении координат: {ex.Message}");
                    return;
                }

              
                try
                {
                    /*
                  var attractions = await GetAttractions(city,lat,lon);
                  if (attractions.Count()== 0)
                  {
                      MessageBox.Show("Достопримечательности не найдены.");
                  }
                  else
                  {
                      richTextBox1.Text = ($"\nДостопримечательности в городе {city}:");
                      foreach (var attraction in attractions)
                      {
                          richTextBox1.Text += ($"\n\nНазвание: {attraction.Name}");
                          richTextBox1.Text += ($"\nАдрес: {attraction.Address}");
                          richTextBox1.Text += ($"\nКатегория: {attraction.Type}");
                          richTextBox1.Text += ($"\nКоординаты: {attraction.Latitude}, {attraction.Longitude}");
                          richTextBox1.Text += ($"\nСсылка: {attraction.LocationLink}");
                          richTextBox1.Text += "\n----------------------------------------";
                      }
                  }
              */

                    await DisplayAttractions(city, lat, lon);
                }
                catch (Exception ex)
                {
                   MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
                

                

            }
        }

        private async Task DisplayAttractions(string city, string lat, string lon)
        {
            var attractions = await GetAttractions(city, lat, lon);

            if (attractions.Count() == 0)
            {
                MessageBox.Show("Достопримечательности не найдены.");
                return;
            }

  
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowCount = 0;


            tableLayoutPanel.HorizontalScroll.Enabled = false;
            tableLayoutPanel.HorizontalScroll.Visible = false;


            int currentRow = 0;
            int currentColumn = 0;

            foreach (var attraction in attractions)
            {
                // Создание контейнера для карточки
                Panel cardPanel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Width = (tableLayoutPanel.Width / tableLayoutPanel.ColumnCount) -45, 
                    Height = 150,
                    Margin = new Padding(5),
                    BackColor = Color.LightSkyBlue
                };

                Label titleLabel = new Label
                {
                    Text = attraction.Name,
                    Font = new Font("Ink Free", 13, FontStyle.Bold),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    Location = new Point(10, 10)
                };
                cardPanel.Controls.Add(titleLabel);

                Label descriptionLabel = new Label
                {
                    Text = $"Описание: {attraction.Type}",
                    Font = new Font("Arial", 13),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    MaximumSize = new Size(cardPanel.Width - 20, 0), 
                    Location = new Point(10, 35)
                };
                cardPanel.Controls.Add(descriptionLabel);

                Label coordinatesLabel = new Label
                {
                    Text = $"Координаты: {attraction.Latitude}, {attraction.Longitude}",
                    Font = new Font("Arial", 10),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    Location = new Point(10, 60)
                };
                cardPanel.Controls.Add(coordinatesLabel);

                LinkLabel routeLink = new LinkLabel
                {
                    Text = "Построить маршрут",
                    Font = new Font("Arial", 12, FontStyle.Underline),
                    AutoSize = true,
                    Location = new Point(10, 85)
                };
                routeLink.LinkClicked += (sender, e) =>
                {
                    System.Diagnostics.Process.Start(attraction.LocationLink);
                };
                cardPanel.Controls.Add(routeLink);

                // Разделительная линия
                Label separator = new Label
                {
                    BorderStyle = BorderStyle.Fixed3D,
                    Height = 2,
                    Width = cardPanel.Width - 20,
                    Location = new Point(10, 110)
                };
                cardPanel.Controls.Add(separator);

                // Добавление строки в TableLayoutPanel, если нужно
                if (currentRow >= tableLayoutPanel.RowCount)
                {
                    tableLayoutPanel.RowCount++;
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Автоматическая высота строки
                }

                // Добавление карточки в таблицу
                tableLayoutPanel.Controls.Add(cardPanel, currentColumn, currentRow);

                // Обновление позиции
                currentColumn++;
                if (currentColumn >= tableLayoutPanel.ColumnCount)
                {
                    currentColumn = 0;
                    currentRow++;
                }
            }
        }

        private async Task<JObject> GetWeatherData(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://api.weatherstack.com/current?access_key={API_KEYweather}&query={city}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(jsonResponse);

                    // Проверяем наличие ошибок в ответе
                    if (data["error"] != null)
                    {
                        throw new Exception(data["error"]["info"].ToString());
                    }

                    // Извлекаем данные о погоде
                    JObject currentWeather = (JObject)data["current"];
                    return currentWeather;
                }
                else
                {
                    throw new Exception($"Ошибка HTTP: {response.StatusCode}");
                }
            }
        }

        private async Task<List<Attraction>> GetAttractions(string city, string lat, string lon)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.geoapify.com/v2/places?categories={categories}&filter=circle:{lon},{lat},5000&limit={numSights}&apiKey={apiKeyMaps}";

                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ошибка при запросе к API: {response.StatusCode}");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(jsonResponse);

                var attractions = json["features"]?
                .Select(feature =>
                {
                    var properties = feature["properties"];
                    var historic = feature["properties"]["historic"];
                    var geometry = feature["geometry"];

                    double attractionLat = geometry?["coordinates"]?[1]?.Value<double>() ?? 0;
                    double attractionLon = geometry?["coordinates"]?[0]?.Value<double>() ?? 0;

                    string locationLink = $"https://www.google.com/maps?q={attractionLat.ToString("G", CultureInfo.InvariantCulture)},{attractionLon.ToString("G", CultureInfo.InvariantCulture)}";


                    return new Attraction
                    {
                        Name = properties?["name"]?.ToString(),
                        Address = properties?["address_line2"]?.ToString(),
                        Type = historic?["type"]?.ToString(),
                        Latitude = geometry?["coordinates"]?[1]?.Value<double>() ?? 0,
                        Longitude = geometry?["coordinates"]?[0]?.Value<double>() ?? 0,
                        LocationLink = locationLink
                    };
                })
                .Where(attraction => !string.IsNullOrEmpty(attraction.Name)) // Фильтруем пустые названия
                .ToList();

                    return attractions;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numSights = numericUpDown1.Value.ToString();
        }

        public class Attraction
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string Type { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string LocationLink { get; set; }
        }

        private void checkedListBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                categories = "";
                foreach (int index in checkedListBox1.CheckedIndices)
                {
                    switch (index)
                    {
                        case 0:
                            categories += "building.tourism,";
                            break;
                        case 1:
                            categories += "building.historic,";
                            break;
                        case 2:
                            categories += "tourism.sights,";
                            break;
                        case 3:
                            categories += "leisure.park,";
                            break;
                        case 4:
                            categories += "building.entertainment,";
                            break;
                    }
                }
                if (checkedListBox1.CheckedIndices.Count > 0)
                {
                    categories = categories.Substring(0, categories.Length - 1);

                }
                else {
                    MessageBox.Show("Выберите хотя бы одну категорию");
                }
                
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }



        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.LinkText,
                UseShellExecute = true
            });
        }
    }
}
