using Newtonsoft.Json;

namespace ApiXMen.Repositories
{
    public class LaboratoryService : ILaboratoryService
    {
        public async ValueTask<bool> DnaCheck(string dnaArray)
        {
            int filas, columnas = 0;


            var dato = JsonConvert.SerializeObject(dnaArray);
            var dato2 = JsonConvert.DeserializeObject<string[]>(dnaArray);

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


            int conteoHorizontal = await ContarHorizontal(matriz2, filas, columnas);
            int conteoVertical = await ContarVertical(matriz2, filas, columnas);
            int conteoDiagonalDerecha = await ContarDiagonalDerecha(matriz2, filas, columnas);
            int datoss = await ContarDiagonalizquierda(matriz2, filas, columnas);

            Console.WriteLine($"El resultado es : {conteoHorizontal + conteoVertical + conteoDiagonalDerecha + datoss}");
            int sumCount = conteoHorizontal + conteoVertical + conteoDiagonalDerecha + datoss;
            if (sumCount > 1)
                return await ValueTask.FromResult(true);
            else 
                return await ValueTask.FromResult(false);

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
    }
}
