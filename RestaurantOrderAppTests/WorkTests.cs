using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantOrderApp;

namespace RestaurantOrderAppTests
{
    [TestClass]
    public class WorkTests
    {
        /// <summary>
        /// Validar se foi informado algum valor na entrada.
        // Regra: A string não pode ser nula ou vazia
        /// </summary>
        [TestMethod]
        public void ValidarInputVazio()
        {
            var work = new Work("");
            Assert.AreEqual(false, work.IsValid);            
        }

        /// <summary>
        /// Valida se o texto informado inicia com Morning ou Night
        /// Regra: Entrada deve conter "Morning" ou "Night" no inicio
        /// </summary>
        [TestMethod]
        public void ValidarInputInicioValido()
        {
            var work = new Work("Morning, 1,2,3");
            Assert.AreEqual(true, work.IsValid);
        }

        /// <summary>
        /// Valida se o texto informado inicia com Morning ou Night
        /// Regra: Entrada deve conter "Morning" ou "Night" no inicio
        /// </summary>
        [TestMethod]
        public void ValidarInputInicioInvalido()
        {
            var work = new Work("1,2,3");
            Assert.AreEqual(false, work.IsValid);
        }
        
        /// <summary>
        /// Validar se foi informado pelo menos uma opção no pedido
        /// Regra: Deve ser informada pelo menos uma opção no pedido
        /// </summary>
        [TestMethod]
        public void ValidarOpcaoDefaultInvalido()
        {
            var work = new Work("Morning");
            work.ProcessOrder();
            Assert.AreEqual(false, work.IsValid);
        }


        /// <summary>
        /// Validar se foi informado pelo menos uma opção no pedido
        /// Regra: Deve ser informada pelo menos uma opção no pedido
        /// </summary>
        [TestMethod]
        public void ValidarOpcaoDefaultValido()
        {
            var work = new Work("Morning,1");
            work.ProcessOrder();
            Assert.AreEqual(true, work.IsValid);
        }

        /// <summary>
        /// Validar se opção informada  é válida e está entre 1 e 4.
        /// Deve ser informada pelo menos uma opção válida no pedido
        /// </summary>
        [TestMethod]
        public void ValidarOpcaoExiste()
        {
            var work = new Work("Morning,6");
            work.ProcessOrder();
            Assert.AreEqual(false, work.IsValid);
        }
        
        /// <summary>
        /// Validar se quantidade de itens é permitida no periodo da manha.
        /// Regra: Exceto para o item Café, os demais não deverão ser pedidos mais de uma vez 
        /// </summary>
        [TestMethod]
        public void ValidarQuantidaeSeForManha()
        {
            var work = new Work("Morning,1,2,2,3");
            work.ProcessOrder();
            Assert.AreEqual(false, work.IsValid);
        }


        /// <summary>
        /// Validar se quantidade de itens é permitida no periodo da noite.
        /// Regra: Exceto para o item batatas, os demais não deverão ser pedidos mais de uma vez 
        /// </summary>
        [TestMethod]
        public void ValidarQuantidaeSeForNoite()
        {
            var work = new Work("Morning,1,2,2,3,4");
            work.ProcessOrder();
            Assert.AreEqual(false, work.IsValid);
        }

        /// <summary>
        /// Validar se quantidade de itens é permitida no periodo da noite.
        /// Regra: Exceto para o item batatas, os demais não deverão ser pedidos mais de uma vez 
        /// </summary>
        [TestMethod]
        public void ValidarRetornoDoPedido()
        {
            string retornoEsperado = "Eggs, Toast, Coffee(x3)";

            var work = new Work("Morning,1,2,3,3,3");
            work.ProcessOrder();
            Assert.AreEqual(retornoEsperado, work._order.ToString());

            retornoEsperado = "Steak, Potato(x2), Wine, Cake";

            work = new Work("Night,1,2,2,3,4");
            work.ProcessOrder();
            Assert.AreEqual(retornoEsperado, work._order.ToString());


            work = new Work("Night,a,A,b,B");
            work.ProcessOrder();
            Assert.AreEqual(false, work.IsValid);


            work = new Work("Night,1,2,a");
            work.ProcessOrder();
            Assert.AreEqual(false, work.IsValid);
        }
    }
}
