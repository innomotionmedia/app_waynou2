using System;
namespace Khaled.CustomComps
{

     public interface IMessage
     {
         void LongAlert(string message);
         void ShortAlert(string message);
         void VeryLongAlert(string message);

     }
}

