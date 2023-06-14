using System;
using System.Threading;
using System.IO.Ports;

namespace ИспользуюБибблиотеку
{
    internal class ReadoutLis200
    {       
        public static SerialPort _serialPort = new SerialPort(); //Обьявление порта 

        public static string initAndOpenCom(string COM, int DataBits, int ReadTime, int WriteTime) // Открывает порт
        {
            try
            {
                _serialPort.PortName = COM;
                _serialPort.BaudRate = 300;
                _serialPort.DataBits = DataBits;
                _serialPort.Parity = Parity.Even;
                _serialPort.StopBits = StopBits.One;
                _serialPort.ReadTimeout = ReadTime;
                _serialPort.WriteTimeout = WriteTime;
                _serialPort.Handshake = Handshake.None;


                _serialPort.Open();
                Console.WriteLine("Порт открыт, все настройки выполнены успешно!");
                return "0";

            }
            catch (Exception e)
            {

                return Convert.ToString(e);
            }

        }

        public static int initAndCloseCom() //Закрывает порт
        {
            try
            {
                _serialPort.Close();
                return 0;
            }
            catch (Exception e)
            {

                return 1;
            }
        }

        public static string speedChangeCom(int speed)
        {
            try
            {
                _serialPort.BaudRate = speed;

                return "0";
               
            }
            catch (Exception e)
            {              
                return Convert.ToString(e);
            }
        }

        public static bool RxByte(byte[] sendBuffer, ref bool FlagActiveOut)
        {
            bool result = false;
            

            try
            {
                // Порт должен быть открыт.
                if (_serialPort.IsOpen)
                {
                    Console.WriteLine("Проверка на открытия порта: Успешно");
                    // Сброс буферов.
                    _serialPort.DiscardInBuffer();
                    _serialPort.DiscardOutBuffer();
                    Console.WriteLine("\t Буфера порта очищенны");

                    if (_serialPort.BytesToRead > 0)
                    {
                        _serialPort.ReadExisting();
                    }                  

                    // Отправка данных.
                    _serialPort.Write(sendBuffer, 0, sendBuffer.Length);                  
                    Console.WriteLine("\tДанные отправленны на порт.");
                    result = true;

                }
            }
            catch
            {
                result = false;
            }

            FlagActiveOut = false;
            return result;
        }

        public static bool TxByte(int waitingForStart, int waitForEnd, CancellationToken cancellationToken, ref byte[] recvBuffer, out int received, ref bool FlagActiveOut)
        {
            
            bool result = false;
            received = -1;

            try
            {
                // Порт должен быть открыт.
                if (_serialPort.IsOpen)
                {
                    Console.WriteLine("Проверка на открытия порта: Успешно");
                    /*// Сброс буферов.
                    _serialPort.DiscardInBuffer();
                    _serialPort.DiscardOutBuffer();*/
                    Console.WriteLine("\t Буфера очищенны");

                    if (_serialPort.BytesToRead > 0)
                    {
                        _serialPort.ReadExisting();
                    }

                    // Установка таймаута начала ответа.
                    _serialPort.ReadTimeout = waitingForStart;

                    /*// Отправка данных.
                    _serialPort.Write(sendBuffer, 0, sendBuffer.Length);
                    Console.WriteLine("\tДанные отправленны.");*/

                    int readed = 0;

                    try
                    {
                        // Ожидание первого байта. Если ничего не придёт за waitingForStart мс, то вылетит TimeoutException.
                        recvBuffer[readed++] = Convert.ToByte(_serialPort.ReadByte());
                        Console.WriteLine("\tОжидаю первый байт");

                        // Установка таймаута для определения завершения ответа.
                        _serialPort.ReadTimeout = waitForEnd;
                        Console.WriteLine("\tУстановил время");

                        try
                        {
                            // Временный буфер для приёма частями.
                            byte[] temp = new byte[16];

                            do
                            {
                                // Чтение и заполнение буфера.
                                // Если ничего не принято, то вылетит TimeoutException.
                                // Если считано меньше или равно размеру буфера, это число возвращается функцией.
                                int b = _serialPort.Read(temp, 0, 16);

                                //Проверка на последние символы.

                                // Копирование в выходной буфер.
                                for (int i = 0; i < b; i++)
                                {
                                    recvBuffer[readed++] = temp[i];
                                }

                                // Повтор до явной отмены.
                                // Также, выход доступен по таймауту чтения.                                 

                            } while (!cancellationToken.IsCancellationRequested);
                        }
                        catch (TimeoutException e)
                        {
                            Console.WriteLine(e.Message + " Цикл закончился ... ...");
                        }
                        finally
                        {
                            // Количество считанного.
                            received = readed;
                            // Возвращаемый результат: принято ли что-нибудь.
                            result = readed > 0;
                        }
                    }
                    catch (TimeoutException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch
            {
                result = false;
            }
            FlagActiveOut = true;
            
            return result;
        }
    }

}
