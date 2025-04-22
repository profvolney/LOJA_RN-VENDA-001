public class Carrinho
{
    public int Id { get; set; }
    public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
    public decimal Total => Itens.Sum(item => item.Preco * item.Quantidade);   
}