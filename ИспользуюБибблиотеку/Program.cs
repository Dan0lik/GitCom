using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DLL_61107;

namespace ИспользуюБибблиотеку
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            bool flagAtive = true;

            //посмотрел все ком порты
            int byteCout = new int();
            byte[] messageRxByte = new byte[255];

            byte[] messageTxByteArgOne = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2F, 0x3F, 0x21, 0xD, 0xA };
            byte[] messageTxByteArgTwo = { 0x06, 0x30, 0x35, 0x31, 0xD, 0xA };

            ReadoutLis200.initAndOpenCom("COM6", 7, 500, 50);

            while (flagAtive == true) {
               
                ReadoutLis200.RxByte(messageTxByteArgOne, ref flagAtive);               
            }
            
            while (flagAtive == false)
            {
                
                ReadoutLis200.TxByte(3000, 40, token, ref messageRxByte, out byteCout, ref flagAtive);               
            }
            
            

            for (int i = 0; i < messageRxByte.Length; i++)
                {
                    Console.Write(Convert.ToChar(messageRxByte[i]));
                }              
          
            for (int i = 0; i < messageRxByte.Length; i++)
            {
                messageRxByte[i] = 0;
            }


            
            
            while (flagAtive == true)
            {
                
                ReadoutLis200.RxByte(messageTxByteArgTwo, ref flagAtive);              
            }

            Thread.Sleep(200);

            while (flagAtive == false) {
                
                ReadoutLis200.speedChangeCom(9600);
                break;
            }

            while (flagAtive == false)
            {
                
                ReadoutLis200.TxByte(3000, 40, token, ref messageRxByte, out byteCout, ref flagAtive);               
            }
            

            for (int i = 0; i < messageRxByte.Length; i++)
                {
                    Console.Write(Convert.ToChar(messageRxByte[i]));
                }             

            Console.WriteLine("Вторая фаза переключения !");

            byte[] messageTxByteSend_1 = {0x01, 0x52, 0x31, 0x02, 0x34, 0x3a, 0x31, 0x37, 0x30, 0x2e, 0x30, 0x2e, 0x30, 0x28, 0x31, 0x29, 0x03};
            byte[] messageTxByteSend_2 = { 0x01, 0x52, 0x31, 0x02, 0x31, 0x3a, 0x31, 0x38, 0x30, 0x2e, 0x30, 0x28, 0x31, 0x29, 0x03 };
            byte[] messageTxByteSend_3 = { 0x01, 0x52, 0x31, 0x02, 0x30, 0x36, 0x3a, 0x33, 0x31, 0x30, 0x5f, 0x31, 0x28, 0x31, 0x29, 0x03 };
            byte[] messageTxByteSend_4 = { 0x01, 0x52, 0x31, 0x02, 0x30 ,0x31, 0x3a, 0x34, 0x30, 0x30, 0x28, 0x31, 0x29, 0x03 };
            byte[] messageTxByteSend_5 = { 0x01, 0x52, 0x31, 0x02, 0x30, 0x31, 0x3a, 0x34, 0x30 ,0x30 ,0x28, 0x31, 0x29, 0x03 };
            byte[] messageTxByteSend_6 = { 0x01, 0x52, 0x31, 0x02, 0x30, 0x36, 0x3a, 0x33, 0x31, 0x30, 0x5f, 0x31, 0x28, 0x31, 0x29, 0x03 };
            byte[] messageTxByteSend_7 = { 0x01, 0x52, 0x31, 0x02, 0x30, 0x36, 0x3a, 0x33, 0x31, 0x30, 0x5f, 0x31, 0x28, 0x31, 0x29, 0x03 };
            byte[] messageTxByteSend_8 = { 0x01, 0x52, 0x31, 0x02, 0x30, 0x36, 0x3a, 0x33, 0x31, 0x30, 0x5f, 0x31, 0x28, 0x31, 0x29, 0x03 };
            ReadoutLis200.GetBCC(ref messageTxByteSend_1);

            while (flagAtive == true)
            {

                ReadoutLis200.RxByte(messageTxByteSend_1, ref flagAtive);            
            }

            while (flagAtive == false)
            {

                ReadoutLis200.TxByte(3000, 40, token, ref messageRxByte, out byteCout, ref flagAtive);
                Console.WriteLine("\tСообщение принято.");
            }



            for (int i = 0; i < messageRxByte.Length; i++)
            {
                Console.Write(Convert.ToChar(messageRxByte[i]));
            }
            Console.WriteLine(" Количество принятых байт - " + byteCout);

            for (int i = 0; i < messageRxByte.Length; i++)
            {
                messageRxByte[i] = 0;
            }
     
           //Цикл начат
            ReadoutLis200.GetBCC(ref messageTxByteSend_2);

            while (flagAtive == true)
            {

                ReadoutLis200.RxByte(messageTxByteSend_2, ref flagAtive);
                Console.WriteLine("\tСообщение отправленно.");
            }

            while (flagAtive == false)
            {

                ReadoutLis200.TxByte(3000, 40, token, ref messageRxByte, out byteCout, ref flagAtive);
                Console.WriteLine("\tСообщение принято.");
            }



            for (int i = 0; i < messageRxByte.Length; i++)
            {
                Console.Write(Convert.ToChar(messageRxByte[i]));
            }
            Console.WriteLine(" Количество принятых байт - " + byteCout);

            for (int i = 0; i < messageRxByte.Length; i++)
            {
                messageRxByte[i] = 0;
            }

            //Цикл Закончен
            ReadoutLis200.GetBCC(ref messageTxByteSend_3);

            while (flagAtive == true)
            {

                ReadoutLis200.RxByte(messageTxByteSend_3, ref flagAtive);
                Console.WriteLine("\tСообщение отправленно.");
            }

            while (flagAtive == false)
            {

                ReadoutLis200.TxByte(3000, 40, token, ref messageRxByte, out byteCout, ref flagAtive);
                Console.WriteLine("\tСообщение принято.");
            }



            for (int i = 0; i < messageRxByte.Length; i++)
            {
                Console.Write(Convert.ToChar(messageRxByte[i]));
            }
            Console.WriteLine(" Количество принятых байт - " + byteCout);

            for (int i = 0; i < messageRxByte.Length; i++)
            {
                messageRxByte[i] = 0;
            }




            ReadoutLis200.initAndCloseCom();
            Console.ReadKey();
        }
    }
}
