using RemitaIntegrate.NET;

namespace SwiftSearch
{
    public interface IGateWayIntegrator
    {
        RemitaResponse CheckRrrStatus(string rrr);
        string PerformNewPaymentHashing(RemitaPost post);
        RemitaResponse PerformPaymentStatusCheck(string orderId);
        RemitaResponse PerformPaymentStatusCheck(string orderId, string url = null);
    }
}