using System.Collections.Generic;

namespace Common.Constants
{
    public static class ErrorMessages
    {
        #region Accounts

        public const string AccountFailureErrorMessage = "Грешка при създаване на акаунта";

        public const string LoginFailureErrorMessage = "Невалиден имейл или парола";

        #endregion

        #region Email

        public const string EmailNotConfirmedErrorMessage = "Имейлът не е потвърден";

        public const string EmailAlreadyConfirmedErrorMessage = "Имейлът вече е потвърден";

        public const string EmailInUseErrorMessage = "Имейлът вече се използва";

        public const string EmailInvalidErrorMessage = "Имейлът е невалиден";

        public const string EmailRequiredErrorMessage = "Имейлът е задължително поле";

        public const string EmailSendingErrorMessage = "Грешка при изпращането на имейл за потвърждение";

        #endregion

        #region Username

        public const string UsernameInUseErrorMessage = "Потребителското име вече се използва";

        public const string UsernameRequiredErrorMessage = "Потребителското име е задължително поле";

        public const string UsernameMinLengthErrorMessage = "Потребителското име трябва да е поне 3 символа";

        public const string UsernameMaxLengthErrorMessage = "Потребителското име трябва да е максимум 15 символа";

        #endregion

        #region Password

        public const string PasswordRequiredErrorMessage = "Паролата е задължително поле";

        public const string PasswordMinLengthErrorMessage = "Паролата трябва да е поне 5 символа";

        public const string PasswordMaxLengthErrorMessage = "Паролата трябва да е поне 15 символа";

        public const string PasswordResetErrorMessage = "Грешка при смяна на паролата";

        #endregion

        #region City

        public const string CityRequiredErrorMessage = "Населеното място е задължително поле";

        #endregion

        #region Common

        public const string CommonErrorMessage = "Възникна грешка. Моля опитайте отново!";

        #endregion

        #region Store

        public const string StoreNotFound = "Магазинът не беше намерен";

        #endregion

        #region Coordinates

        public const string CoordinatesRequiredErrorMessage = "Координате са задължително поле";

        #endregion

        #region WorkingHours

        public const string WorkingHoursRequiredErrorMessage = "Работното време е задължително поле";

        #endregion

        #region Description

        public const string DescriptionRequiredErrorMessage = "Описанието е задължително поле";

        #endregion

        #region PhoneNumber

        public const string PhoneNumberRequiredErrorMessage = "Телефонният номер е задължително поле";

        #endregion

        #region General

        public const string GeneralSomethingWentWrong = "Нещо се обърка";

        #endregion

        #region ConfirmEmail

        public const string ConfirmEmailError = "Нещо се обърка, при потвърждаването на имейла.";

        #endregion

        #region ChangeEmail

        public const string ChangeEmailErrorMessage = "Нещо се обърка, при смяна на имейла.";
        public const string ChangeEmailDifferentEmailsErrorMessage = "Новият имейл трябва да е различен от стария";

        #endregion

        #region Token

        public const string AccessTokenInvalidErrorMessage = "Невалидна сесия";

        #endregion
    }
}
