using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwiftSearch.RemitaResources.Infrastructure
{

    public class SampleConfig : IntegrateConfig
    {
        public SampleConfig(string merchantId, string serviceTypeId, string apiKey)
        {
            MerchantId = merchantId;
            ServiceTypeId = serviceTypeId;
            ApiKey = apiKey;
        }
    }
}