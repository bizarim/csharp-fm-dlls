using fmLibrary;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace fmServerCommon.InAppBilling
{
    public class IOSJson
    {
        public JsonRecipt receipt { get; set; }
        public string status { get; set; }
    }

    public class JsonRecipt
    {
        //public DateTime original_purchase_date_pst { get; set; }
        //public int purchase_date_ms { get; set; }
        //public string unique_identifier { get; set; }
        public string product_id { get; set; }
        public string bid { get; set; }
        public string transaction_id { get; set; }
        //"receipt":{ "original_purchase_date_pst":"2016-09-28 00:22:29 America/Los_Angeles", "purchase_date_ms":"1475047349523",
        //"unique_identifier":"ab52c0d2d2916a0cda41957e7f0803cc68baa16d",
        //"original_transaction_id":"1000000238905265",
        //"bvrs":"1.62",
        //"transaction_id":"1000000238905265",
        //"quantity":"1",
        //"unique_vendor_identifier":"A0009FA4-14B1-4A90-828E-0B928EBF0663",
        //"item_id":"1159239877",
        //"product_id":"shop_ruby_1",
        //"purchase_date":"2016-09-28 07:22:29 Etc/GMT",
        //"original_purchase_date":"2016-09-28 07:22:29 Etc/GMT",
        //"purchase_date_pst":"2016-09-28 00:22:29 America/Los_Angeles",
        //"bid":"com.codecore.indiepia",
        //"original_purchase_date_ms":"1475047349523"
    }

    public class IOSReceipt
    {
        public string OriginalTransactionId { get; set; }
        public string Bvrs { get; set; }
        public string ProductId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public string BundleIdentifier { get; set; }
        public DateTime? OriginalPurchaseDate { get; set; }
        public string TransactionId { get; set; }
        public int Status { get; set; }

        public IOSReceipt(string jsonData)
        {
            // 필요한것
            //status
            //    receipt
            //    product_id
            //    bid
            //    transaction_id
            Logger.Debug("IOSReceipt: {0}", jsonData);

            //{"receipt":{ "original_purchase_date_pst":"2016-09-28 00:22:29 America/Los_Angeles", "purchase_date_ms":"1475047349523", "unique_identifier":"ab52c0d2d2916a0cda41957e7f0803cc68baa16d", "original_transaction_id":"1000000238905265", "bvrs":"1.62", "transaction_id":"1000000238905265", "quantity":"1", "unique_vendor_identifier":"A0009FA4-14B1-4A90-828E-0B928EBF0663", "item_id":"1159239877", "product_id":"shop_ruby_1", "purchase_date":"2016-09-28 07:22:29 Etc/GMT", "original_purchase_date":"2016-09-28 07:22:29 Etc/GMT", "purchase_date_pst":"2016-09-28 00:22:29 America/Los_Angeles", "bid":"com.codecore.indiepia", "original_purchase_date_ms":"1475047349523"},
            // "status":0}

            IOSJson dicJson = null;
            try
            {
                dicJson = new JavaScriptSerializer().Deserialize<IOSJson>(jsonData);
            }
            catch (Exception ex)
            {
                Logger.Debug("EX: {0}", ex.ToString());
            }

            if (dicJson == null)
                Logger.Debug("dicJson: null");

            int status = -1;

            int.TryParse(dicJson.status, out status);
            Status = status;

            Logger.Debug("IOSReceipt->Status: {0}", Status);

            if (Status == 21007)
                return;

            // Receipt
            //Dictionary<string, string> dicRecipt = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(dicJson.receipt);

            //this.OriginalTransactionId = json["original_transaction_id"].ToString();
            //this.Bvrs = json["bvrs"].ToString();
            ProductId = dicJson.receipt.product_id;// dicRecipt["product_id"];
            //DateTime purchaseDate = DateTime.MinValue;
            //if (DateTime.TryParseExact(json["purchase_date"].ToString().Replace(" Etc/GMT", string.Empty).Replace("\"", string.Empty).Trim(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out purchaseDate))
            //	this.PurchaseDate = purchaseDate;
            //DateTime originalPurchaseDate = DateTime.MinValue;
            //if (DateTime.TryParseExact(json["original_purchase_date"].ToString().Replace(" Etc/GMT", string.Empty).Replace("\"", string.Empty).Trim(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out originalPurchaseDate))
            //	this.OriginalPurchaseDate = originalPurchaseDate;
            int quantity = 1;
            //int.TryParse(json["quantity"].ToString().Trim('\"'), out quantity);
            Quantity = quantity;
            BundleIdentifier = dicJson.receipt.bid; // dicRecipt["bid"];
            TransactionId = dicJson.receipt.transaction_id; //dicRecipt["transaction_id"];

            Logger.Debug("IOSReceipt->ProductId: {0}", ProductId);
            Logger.Debug("IOSReceipt->BundleIdentifier: {0}", BundleIdentifier);
            Logger.Debug("IOSReceipt->TransactionId: {0}", TransactionId);
        }
    }
}
