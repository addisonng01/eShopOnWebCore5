using Ardalis.GuardClauses;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
{
    public class BasketItem : BaseEntity
    {

        public decimal UnitPrice { get; private set; }
        public decimal Tax { get; private set; }
        public double GrandTotal { get; private set; }
        public int Quantity { get; private set; }
        public int CatalogItemId { get; private set; }
        public int BasketId { get; private set; }

        public BasketItem(int catalogItemId, int quantity, decimal unitPrice, decimal tax, double grandTotal)
        {
            CatalogItemId = catalogItemId;
            UnitPrice = unitPrice;
            SetQuantity(quantity);
            Tax = tax;
            GrandTotal = grandTotal;
        }

        public void calcTotal(decimal unitPrice, decimal tax, double grandTotal)
        {
            grandTotal = (double)(unitPrice + (unitPrice * tax));
        }

        public void AddQuantity(int quantity)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

            Quantity += quantity;
        }

        public void SetQuantity(int quantity)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

            Quantity = quantity;
        }
    }
}
