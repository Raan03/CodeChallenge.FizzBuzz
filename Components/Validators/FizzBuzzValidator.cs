using CodeChallenge.FizzBuzz.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Timers;

namespace CodeChallenge.FizzBuzz.Components.Validators
{
    public class FizzBuzzValidator : ComponentBase
    {
        private ValidationMessageStore validationMessageStore;

        [CascadingParameter]
        private EditContext CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(FizzBuzzValidator)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(FizzBuzzValidator)} " +
                    $"inside an {nameof(EditForm)}.");
            }
            validationMessageStore = new ValidationMessageStore(CurrentEditContext);

            // Attach event handlers to the EditContext
            CurrentEditContext.OnFieldChanged += (s,e) => ValidateField(e.FieldIdentifier);
            CurrentEditContext.OnValidationRequested += (s, e) => ValidateAllFields();
        }

        private void ValidateAllFields()
        {
            validationMessageStore.Clear();
            var model = CurrentEditContext.Model as FizzBuzzModel ?? throw new InvalidOperationException("Model is not of type FizzBuzzModel");

            ValidateField(new FieldIdentifier(model, nameof(FizzBuzzModel.FizzValue)));
            ValidateField(new FieldIdentifier(model, nameof(FizzBuzzModel.BuzzValue)));
            ValidateField(new FieldIdentifier(model, nameof(FizzBuzzModel.StopValue)));

            CurrentEditContext.NotifyValidationStateChanged();
        }

        private void ValidateField(FieldIdentifier fieldIdentifier)
        {
            // Extra Credit Validation:
            // Buzz Greater Than Fizz: BuzzValue must always be greater than FizzValue.
            //
            // StopValue Minimum: StopValue must be equal to or greater than the product of FizzValue and BuzzValue to ensure at least one FizzBuzz appears.
            // 
            // FizzBuzz Generation – Based on the user-defined FizzValue, BuzzValue, and StopValue, this area of functionality will generate the FizzBuzz sequence. For example, with FizzValue = 3, BuzzValue = 5, and StopValue = 100, the output might look like["1", "2", "Fizz", "4", "Buzz", ..., "14", "FizzBuzz"].
            // 
            // FizzBuzz Display – Once generated, the FizzBuzz sequence will be displayed interactively on the page in response to user input, creating a smooth, real-time experience without reloading the page.
 
            var model = CurrentEditContext.Model as FizzBuzzModel ?? throw new InvalidOperationException("Model is not of type FizzBuzzModel");
            
            validationMessageStore.Clear(fieldIdentifier);

            if (fieldIdentifier.FieldName == nameof(FizzBuzzModel.FizzValue))
            {
                if (model.FizzValue >= model.BuzzValue)
                {
                    validationMessageStore.Add(fieldIdentifier, $"FizzValue ({model.FizzValue}) must be less than BuzzValue ({model.BuzzValue}).");
                }
            }
            else if (fieldIdentifier.FieldName == nameof(FizzBuzzModel.BuzzValue))
            {
                if (model.FizzValue >= model.BuzzValue)
                {
                    validationMessageStore.Add(fieldIdentifier, $"BuzzValue ({model.BuzzValue}) must be higher than FizzValue ({model.FizzValue}).");
                }
            }
            else if (fieldIdentifier.FieldName == nameof(FizzBuzzModel.StopValue))
            {
                if (model.StopValue < model.FizzValue * model.BuzzValue)
                {
                    validationMessageStore.Add(fieldIdentifier, $"StopValue must be equal to or greater than the product of FizzValue and BuzzValue ({model.FizzValue * model.BuzzValue}).");
                }
            }
        }
    }
}
