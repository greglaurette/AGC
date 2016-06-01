using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmherstGolfClub.Models
{
    public class CanadianProvinces
    {
        public static readonly IDictionary<string, string> ProvinceDictionary = new Dictionary<string, string>
        {
            {"NOVA SCOTIA", "NS"},
            {"NEW BRUNSWICK", "NB"},
            {"PRINCE EDWARD ISLAND", "PE"},
            {"--------------------", "NS"},
            {"ALBERTA", "AB"},
            {"BRITISH COLUMBIA", "BC"},
            {"MANITOBA", "MB"},            
            {"NEWFOUNDLAND AND LABRADOR", "NL"},
            {"NORTHWEST TERRITORIES", "NT"},            
            {"NUNAVUT", "NU"},
            {"ONTARIO", "ON"},            
            {"QUEBEC", "QC"},
            {"SASKATCHEWAN", "SK"},
            {"YUKON", "YT"}
        };

        public static SelectList ProvinceSelectList
        {
            get { return new SelectList(ProvinceDictionary, "Value", "Key"); }
        }

    }
}