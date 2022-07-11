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

            //Se recorreo vector en todas las direcciones y se suman las secuencias que coinciden con un mutante
            int countHorizontal = await CountHorizontal(arrayData.SerializedArray, arrayData.Rows, arrayData.Columns);
            int countUpright = await CountUpright(arrayData.SerializedArray, arrayData.Rows, arrayData.Columns);
            int countRightDiagonal = await CountRightDiagonal(arrayData.SerializedArray, arrayData.Rows, arrayData.Columns);
            int countLeftDiagonal = await CountLeftDiagonal(arrayData.SerializedArray, arrayData.Rows, arrayData.Columns);
            int sumCount = countHorizontal + countUpright + countRightDiagonal + countLeftDiagonal;
            
            //Si la suma es mayor a 1 se asigna true ó false
            if (sumCount > 1)
                resultTest = true;
            else
                resultTest = false;

            //Se crea objeto para ser almacenado en la base de datos
            DnaResult dnaResult = new DnaResult()
            {
                Id = Guid.NewGuid().ToString(),
                DnaVerified = dnaArray,
                TestResult = resultTest
            };
            await SqlService.SaveDnaResult(dnaResult);
            
            return await ValueTask.FromResult(resultTest);
        }

        private async ValueTask<int> CountHorizontal(string[,] data, int rows, int columns)
        {
            string[] auxVector = new string[4];
            int generalCounter = 0;

            //Se recorreo matriz
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns - 3; j++)
                {
                    //Se asignan letras que estan horizontales
                    auxVector[0] = data[i, j];
                    auxVector[1] = data[i, j + 1];
                    auxVector[2] = data[i, j + 2];
                    auxVector[3] = data[i, j + 3];

                    // Se valida si la las cuatro letras del vector son iguales
                    int countSequence = await ValidateSequence(auxVector);

                    //Si las cuatro letras son iguales se realiza el conteo
                    if (countSequence == 4)
                        generalCounter++;
                }
            }
            return await ValueTask.FromResult(generalCounter);
        }

        private async ValueTask<int> CountUpright(string[,] data, int rows, int columns)
        {
            string[] auxVector = new string[4];
            int generalCounter = 0;

            //Se recorreo matriz
            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = 0; j < columns; j++)
                {   // se asignan al vector las lectras que estan verticales
                    auxVector[0] = data[i, j];
                    auxVector[1] = data[i + 1, j];
                    auxVector[2] = data[i + 2, j];
                    auxVector[3] = data[i + 3, j];

                    // Se valida si la las cuatro letras del vector son iguales
                    int countSequence = await ValidateSequence(auxVector);

                    //Si las cuatro letras se realiza el conteo
                    if (countSequence == 4)
                        generalCounter++;
                }

            }
            return await ValueTask.FromResult(generalCounter);
        }

        private async ValueTask<int> CountRightDiagonal(string[,] data, int rows, int columns)
        {

            string[] auxVector = new string[4];
            int generalCounter = 0;

            // Se recorre matriz con las letras
            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = 0; j < columns - 3; j++)
                {
                    //Se llena un vector las letras que estan en la diagonal
                    auxVector[0] = data[i, j];
                    auxVector[1] = data[i + 1, j + 1];
                    auxVector[2] = data[i + 2, j + 2];
                    auxVector[3] = data[i + 3, j + 3];

                    // Se valida si la las cuatro letras del vector son iguales
                    int countSequence = await ValidateSequence(auxVector);

                    //Se realiza conteo si las cuatro letras del vector son iguales.
                    if (countSequence == 4)
                        generalCounter++;
                }

            }
            return await ValueTask.FromResult(generalCounter);
        }

        private async ValueTask<int> CountLeftDiagonal(string[,] data, int rows, int columns)
        {

            string[] auxVector = new string[4];
            int generalCounter = 0;
            // Se recorre matriz de derecha a izquierda por las columnas
            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = columns - 1; j > 0 + 2; j--)
                {   //Se llena un vector las letras que estan en la diagonal
                    auxVector[0] = data[i, j];
                    auxVector[1] = data[i + 1, j - 1];
                    auxVector[2] = data[i + 2, j - 2];
                    auxVector[3] = data[i + 3, j - 3];

                    // Se valida si la las cuatro letras del vector son iguales
                    int countSequence = await ValidateSequence(auxVector);
       
                    // si las letras son iguales se realiza el conteo.
                    if (countSequence == 4)
                        generalCounter++;
                }

            }
           return await ValueTask.FromResult(generalCounter);
        }

        private async ValueTask<DeserializedObjectDto> ProcessArray(string data)
        {
            int rows, columns = 0;

            //Se deserializa json recibido y se convierte a vector
            var deserializeData = JsonConvert.DeserializeObject<string[]>(data);

            //Se define dimesiones de la matriz
            rows = deserializeData.Length;
            columns = deserializeData[0].Length;
            string[,] arrayData = new string[rows, columns];

            //Se recorre el vector deserializado para separar las letras y almacenarlas en la matriz
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    string auxiliary = deserializeData[i].Substring(j, 1).ToUpper();

                    //Se valida que solo permita las letra T C G A
                    if (auxiliary == "T" || auxiliary == "C" || auxiliary == "G" || auxiliary == "A")
                        arrayData[i, j] = auxiliary;
                    else
                        throw new Exception("Las letras permitidas son A G C T");
                }
            }

            //Se crea una entidad con la matriz y sus dimensiones.
            DeserializedObjectDto DeserializedObjectDto = new DeserializedObjectDto()
            {
                Rows = rows,
                Columns = columns,
                SerializedArray = arrayData
            };
            return await ValueTask.FromResult(DeserializedObjectDto);
        }

        private async ValueTask<int> ValidateSequence(string[] sequence)
        {
            string aux = "";
            int count = 1;
            //se evalua si las cuatro letras del vector son iguales
            for (int y = 0; y < 4; y++)
            {
                if (sequence[y] == aux)
                {
                    count++;
                }
                else
                {
                    aux = sequence[y];
                    count = 1;
                }
            }
            return await ValueTask.FromResult(count);
        }
    }
}
