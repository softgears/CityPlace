// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: PlaceDetailsModel.cs
// 
// Created by: ykors_000 at 24.03.2014 10:41
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// Расширенная модель данных заведения
    /// </summary>
    public class PlaceDetailsModel: PlaceModel
    {
        /// <summary>
        /// Описание
        /// </summary>
        public string description { get; set; }

        public string phone1 { get; set; }

        public string phone2 { get; set; }

        public string phone3 { get; set; }

        public string fax { get; set; }

        public string site { get; set; }

        public string email { get; set; }

        public double? lat { get; set; }

        public double? lon { get; set; }

        public string work_time { get; set; }

        public bool cash { get; set; }

        public bool cashless { get; set; }

        public bool booking { get; set; }

        public bool ordering { get; set; }

        public bool wifi { get; set; }

        public bool vip { get; set; }

        public bool liveMusic { get; set; }

        public bool businessLunch { get; set; }

        public decimal? check { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public PlaceDetailsModel(Place model) : base(model)
        {
            description = model.Description;
            phone1 = model.Phone1;
            phone2 = model.Phone2;
            phone3 = model.Phone3;
            fax = model.Fax;
            site = model.Site;
            email = model.Email;
            lat = model.Latitude;
            lon = model.Longitude;
            work_time = model.WorkTime;
            cash = model.CashPayments;
            cashless = model.CashlessPayments;
            booking = model.Booking;
            ordering = model.Ordering;
            wifi = model.WiFi;
            vip = model.VIPSection;
            liveMusic = model.LiveMusic;
            businessLunch = model.BusinessLunch;
            check = model.AverageCheck;
        }
    }
}