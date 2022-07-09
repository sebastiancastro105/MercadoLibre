using ApiXMen.Models;
using ApiXMen.Models.Dtos;
using Newtonsoft.Json;

namespace ApiXMen.Repositories
{
    public class LaboratoryService : ILaboratoryService
    {
        private readonly ISqlService SqlService;

        public LaboratoryService(ISqlService sqlService)
        {
            SqlService = sqlService;
        }
        public async ValueTask<bool> DnaCheck(string dnaArray)
        {
            bool resultTest = false;
            var arrayData = await ProcessArray(dnaArray);

            int conteoHorizontal = await ContarHorizontal(arrayData.SerializedArray, arrayData.Rows, arrayData.Columns);
            int conteoVertical = await ContarVertical(arrayData.SerializedArray, arrayData.Rows, arrayData.Columns);
            int conteoDiagonalDerecha = await ContarDiagonalDerecha(arrayData.SerializedArray, arrayData.Rows, arrayData.Columns);
            int datoss = await ContarDiagonalizquierda(arrayData.SerializedArray, arrayData.Rows, arrayData.Columns);
            int sumCount = conteoHorizontal + conteoVertical + conteoDiagonalDerecha + datoss;

            if (sumCount > 1)
                resultTest = true;
            else
                resultTest = false;

            DnaResult dnaResult = new DnaResult()
            {
                Id = Guid.NewGuid().ToString(),
                DnaVerified = dnaArray,
                TestResult = resultTest
            };
            await SqlService.SaveDnaResult(dnaResult);
            
            return await ValueTask.FromResult(resultTest);
        }

        private async ValueTask<int> ContarHorizontal(string[,] matriz, int filas, int columnas)
        {
            string[] vectorAxu = new string[4];
            int contadorGeneral = 0;

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas - 3; j++)
                {
                    vectorAxu[0] = matriz[i, j];
                    vectorAxu[1] = matriz[i, j + 1];
                    vectorAxu[2] = matriz[i, j + 2];
                    vectorAxu[3] = matriz[i, j + 3];

                    string aux = "";
                    int count = 1;

                    for (int y = 0; y < 4; y++)
                    {
                        if (vectorAxu[y] == aux)
                        {
                            count++;
                        }
                        else
                        {
                            aux = vectorAxu[y];
                            count = 1;
                        }
                    }

                    if (count == 4)
                        contadorGeneral++;
                }

            }
            return await ValueTask.FromResult(contadorGeneral);
        }

        private async ValueTask<int> ContarVertical(string[,] matriz, int filas, int columnas)
        {
            string[] vectorAxu = new string[4];
            int contadorGeneral = 0;

            for (int i = 0; i < filas - 3; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    vectorAxu[0] = matriz[i, j];
                    vectorAxu[1] = matriz[i + 1, j];
                    vectorAxu[2] = matriz[i + 2, j];
                    vectorAxu[3] = matriz[i + 3, j];

                    string aux = "";
                    int count = 1;

                    for (int y = 0; y < 4; y++)
                    {
                        if (vectorAxu[y] == aux)
                        {
                            count++;
                        }
                        else
                        {
                            aux = vectorAxu[y];
                            count = 1;
                        }
                    }

                    if (count == 4)
                        contadorGeneral++;
                }

            }
            return await ValueTask.FromResult(contadorGeneral);
        }

        private async ValueTask<int> ContarDiagonalDerecha(string[,] matriz, int filas, int columnas)
        {

            string[] vectorAxu = new string[4];
            int contadorGeneral = 0;

            for (int i = 0; i < filas - 3; i++)
            {
                for (int j = 0; j < columnas - 3; j++)
                {
                    vectorAxu[0] = matriz[i, j];
                    vectorAxu[1] = matriz[i + 1, j + 1];
                    vectorAxu[2] = matriz[i + 2, j + 2];
                    vectorAxu[3] = matriz[i + 3, j + 3];

                    string aux = "";
                    int count = 1;

                    for (int y = 0; y < 4; y++)
                    {
                        if (vectorAxu[y] == aux)
                        {
                            count++;
                        }
                        else
                        {
                            aux = vectorAxu[y];
                            count = 1;
                        }
                    }

                    if (count == 4)
                        contadorGeneral++;
                }

            }
            return await ValueTask.FromResult(contadorGeneral);
        }

        private async ValueTask<int> ContarDiagonalizquierda(string[,] matriz, int filas, int columnas)
        {

            string[] vectorAxu = new string[4];
            int contadorGeneral = 0;

            for (int i = 0; i < filas - 3; i++)
            {
                for (int j = columnas - 1; j > 0 + 2; j--)
                {
                    vectorAxu[0] = matriz[i, j];
                    vectorAxu[1] = matriz[i + 1, j - 1];
                    vectorAxu[2] = matriz[i + 2, j - 2];
                    vectorAxu[3] = matriz[i + 3, j - 3];

                    string aux = "";
                    int count = 1;

                    for (int y = 0; y < 4; y++)
                    {
                        if (vectorAxu[y] == aux)
                        {
                            count++;
                        }
                        else
                        {
                            aux = vectorAxu[y];
                            count = 1;
                        }
                    }

                    if (count == 4)
                        contadorGeneral++;
                }

            }
           return await ValueTask.FromResult(contadorGeneral);
        }

        private async ValueTask<DeserializedObjectDto> ProcessArray(string data)
        {
            int filas, columnas = 0;

            var dato = JsonConvert.SerializeObject(data);
            var dato2 = JsonConvert.DeserializeObject<string[]>(data);

            filas = dato2.Length;
            columnas = dato2[0].Length;
            string[,] matriz2 = new string[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matriz2[i, j] = dato2[i].Substring(j, 1);
                }
            }

            DeserializedObjectDto DeserializedObjectDto = new DeserializedObjectDto()
            {
                Rows = filas,
                Columns = columnas,
                SerializedArray = matriz2
            };
            return await ValueTask.FromResult(DeserializedObjectDto);
        }
    }
}
