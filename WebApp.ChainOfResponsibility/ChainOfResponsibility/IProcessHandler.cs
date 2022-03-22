using System;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public interface IProcessHandler
    {
        //Zincirdeki bir sonraki adımı temsil edecek bir interface yazıyoruz...
        IProcessHandler SetNext(IProcessHandler processHandler);

        //İstediğimiz her datayı gönderebilelim diye Object olarak belirledik.
        Object handle(Object o);
        
    }
}
