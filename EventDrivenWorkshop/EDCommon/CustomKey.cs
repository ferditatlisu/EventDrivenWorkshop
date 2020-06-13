using System;
using System.Collections.Generic;
using System.Text;

namespace EDCommon
{
    public class CustomKey
    {
        public const string RABBITMQ_BASE_ENDPOINT = "rabbitmq://rabbitmq";
        public const string JSON_CONTENT_TYPE = "application/json";

        #region QUEUE_NAMES

        public const string RABBITMQ_PRICE_ORDER_REQUEST_ENDPOINT = "priceOrderRequestQueue";
        public const string RABBITMQ_PLACE_ORDER_REQUEST_ENDPOINT = "placeOrderRequestQueue";
        public const string RABBITMQ_PRICE_ORDER_RESPONSE_ENDPOINT = "priceOrderResponseQueue";
        public const string RABBITMQ_PLACE_ORDER_RESPONSE_ENDPOINT = "placeOrderResponseQueue";
        public const string RABBITMQ_ORDER_RESPONSE_ENDPOINT = "orderResponseQueue";


        #endregion
    }
}
