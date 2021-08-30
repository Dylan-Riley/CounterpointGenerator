using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    /**
     * implementation of the layer that translates user input to intermediate representation.
     */
    public class InputTranslator : IInputTranslator
    {
        public InputTranslator()
        {
        }

        private void MelodyInvalid()
        {
            Console.WriteLine("Melody line input not valid");
            Console.WriteLine("Expected \"int pitch space double duration\" separated by commas!");
            Console.WriteLine("Insert a MelodyLine: ");
        }

        public Task<IInput> GetInput()
        {
            // Melody Line input
            Console.WriteLine("Input Acceptor: Type \"quit\" to quit");
            Console.WriteLine("Insert a MelodyLine: ");
            // Input expected to be something like "0 16, 0 16, 0 16"
            // (int space double comma space)* (int space double)
            bool checkMelody = true;
            List<Note> inputList = new List<Note>();
            while (checkMelody)
            {
                inputList.Clear();
                string melodyInput = Console.ReadLine();

                if(melodyInput == "quit" || melodyInput == "exit" || melodyInput == "q")
                {
                    throw new TaskCanceledException();
                }

                string[] outerSplit = melodyInput.Split(", ");

                if (outerSplit.Length == 1)
                {
                    // If the split fails to actually split on anything
                    MelodyInvalid();
                }
                else
                {
                    foreach (string s in outerSplit)
                    {
                        string[] innerSplit = s.Split(' ');

                        /*
                         * In the TryParse checking to collapse them & needs to be used instead of &&
                         * in order to make sure newPitch and newDuration get fully declared and can be used later
                         * 
                         * If length checking is also collapsed into this single if statement there is then
                         * a possible IndexOutOfBounds which I don't want to deal with so it needs to be its
                         * own check first that happens before TryParse and won't allow them to run if failed
                         */
                        if (innerSplit.Length != 2)
                        {
                            // If the split ends in too many or too few values
                            MelodyInvalid();
                            break;
                        }
                        if(  !int.TryParse(innerSplit[0], out int newPitch)
                           & !Double.TryParse(innerSplit[1], out double newDuration))
                        {
                            // Or either value cannot be parsed
                            MelodyInvalid();
                            break;
                        }

                        // TryParse sets the value of these variables on success
                        inputList.Add(new Note(newPitch, newDuration));

                        if (inputList.Count == outerSplit.Length)
                        {
                            // End condition, handled everything in input
                            checkMelody = false;
                        }
                    }
                }
            }

            // Generation time input
            Console.WriteLine("Insert a prefered generating duration(in seconds): ");
            double userPreference = 0;
            bool checkTime = true;
            while (checkTime)
            {
                string inputPreference = Console.ReadLine();
                if (inputPreference == "")
                {
                    // Catch this first as it does weird things with TryParse
                    userPreference = Constants.DEFAULT_TIME_LIMIT;
                    checkTime = false;
                }
                else if (Double.TryParse(inputPreference, out userPreference))
                {
                    // TryParse sets the out parameter on check
                    // Only returns true if a double was successfully parsed
                    checkTime = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid double for time in seconds!");
                    Console.WriteLine("Insert a prefered generating duration(in seconds): ");
                }
            }

            var input = new Input(new MelodyLine(inputList), userPreference);
            Console.WriteLine(input.ToString());
            return Task.FromResult<IInput>(input);
        }

    }
}