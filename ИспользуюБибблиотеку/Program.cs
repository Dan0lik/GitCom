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

            ReadoutLis200.initAndOpenCom("COM13", 7, 500, 50);

            while (flagAtive == true) {
               
                ReadoutLis200.RxByte(messageTxByteArgOne, ref flagAtive);
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


            
            
            while (flagAtive == true)
            {
                
                ReadoutLis200.RxByte(messageTxByteArgTwo, ref flagAtive);
                Console.WriteLine("\tСообщение отправленно.");
            }

            Thread.Sleep(200);

            while (flagAtive == false) {
                
                ReadoutLis200.speedChangeCom(9600);
                break;
            }

            while (flagAtive == false)
            {
                
                ReadoutLis200.TxByte(3000, 40, token, ref messageRxByte, out byteCout, ref flagAtive);
                Console.WriteLine("\tСообщение принято..");
            }
            

            for (int i = 0; i < messageRxByte.Length; i++)
                {
                    Console.Write(Convert.ToChar(messageRxByte[i]));
                }
                Console.WriteLine("Количество принятых байт - " + byteCout);

            Console.WriteLine("Вторая фаза переключения !");

            byte[] messageTxByteSend_1 = { 0x06, 0x30, 0x35, 0x31, 0xD, 0xA };





            ReadoutLis200.initAndCloseCom();
            Console.ReadKey();
        }
    }
}
