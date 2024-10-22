﻿namespace Common.Constants
{
    public static class ErrorMessages
    {
        #region Accounts

        public const string AccountFailureErrorMessage = "Грешка при създаване на акаунта";

        public const string LoginFailureErrorMessage = "Невалиден имейл или парола";

        public const string NotAllowedErrorMessage = "Нямате достъп до този ресурс";

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

        public const string UsernameMaxLengthErrorMessage = "Потребителското име трябва да е максимум 30 символа";

        #endregion

        #region Password

        public const string PasswordRequiredErrorMessage = "Паролата е задължително поле";

        public const string PasswordMinLengthErrorMessage = "Паролата трябва да е поне 5 символа";

        public const string PasswordMaxLengthErrorMessage = "Паролата трябва да е поне 15 символа";

        public const string PasswordResetErrorMessage = "Грешка при смяна на паролата";

        public const string PasswordRequirmentsErrorMessage = "Паролата трябва да има малка буква и поне 5 символа";

        #endregion

        #region City

        public const string CityRequiredErrorMessage = "Населеното място е задължително поле";

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

        public const string PhoneNumberFormatErrorMessage = "Телефонният номер е в невалиден формат";

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

        public const string ConfirmTokenInvalidErrorMessage = "Невалиден токен, изпратете нов линк за потвърждаване";

        #endregion

        #region User

        public const string UserNotFoundErrorMessage = "Потребителят не беше намерен";

        #endregion

        #region FirstName

        public const string FirstNameMinLengthErrorMessage = "Първото име трябва да е поне 3 символа";

        public const string FirstNameMaxLengthErrorMessage = "Първото име трябва да е най-много 30 символа";

        #endregion

        #region LastName

        public const string LastNameMinLengthErrorMessage = "Фамилията трябва да е поне 3 символа";

        public const string LastNameMaxLengthErrorMessage = "Фамилията трябва да е най-много 30 символа";

        #endregion

        #region ProfilePicture

        public const string InvalidProfilePictureFormatErrorMessage = "Профилната снимка е в невалиден формат";

        public const string ProfilePictureRequiredErrorMessage = "Липсва профилна снимка";

        #endregion

        #region TreeImage

        public const string TreeImageRequiredErrorMessage = "Въведете поне една снимка на дървото";

        public const string TreeImageNotFoundErrorMessage = "Снимката не беше намерена";

        public const string TreeImageInvalidFormatErrorMessage = "Снимката е в невалиден формат";

        #endregion

        #region Image

        public const string ImageRequiredErrorMessage = "Снимката липсва";

        #endregion

        #region Tree

        public const string TreeNicknameRequiredErrorMessage = "Името на дървото е задължително";

        public const string TreeNicknameInUseErrorMessage = "Името на дървото е заето";

        public const string TreeNicknameMinLengthErrorMessage = "Името на дървото трябва да е поне 3 символа";

        public const string TreeNicknameMaxLengthErrorMessage = "Името на дървото трябва да е максимум 20 символа";

        public const string TreeTypeRequiredErrorMessage = "Типът на дървото е задължителен";

        public const string TreeCategoryRequiredErrorMessage = "Категорията на дървото е задължителна";

        public const string TreeLocationRequiredErrorMessage = "Локацията на дървото е задължителна";

        public const string TreeNotFoundErrorMessage = "Не съществува такова дърво";

        #endregion

        #region Waterings

        public const string WateringDelayErrorMessage = "Моля изчакайте преди да полеете дървото отново";

        #endregion

        #region TreeReports

        public const string TreeReportSelfReportErrorMessage = "Не може да докладвате собственото си дърво";

        public const string TreeReportDublicateErrorMessage = "Вече сте докладвали това дърво за този проблем";

        public const string TreeReportNotFoundErrorMessage = "Вече сте докладвали това дърво за този проблем";

        #endregion

        #region TreePosts

        public const string TreePostNotFoundErrorMessage = "Постът не беше намерен";

        public const string TreePostContentRequiredErrorMessage = "Липсва съдържание на поста";

        public const string TreePostContentMaxLengthErrorMessage = "Постът е твърде дълъг";

        #endregion

        #region TreePostReactions

        public const string TreePostReactionNotFoundErrorMessage = "Реакцията не съществува";

        public const string TreePostAlreadyReactedErrorMessage = "Потребителят вече е реагирал на поста";

        #endregion

        #region TreePostReply

        public const string TreePostReplyNotFoundErrorMessage = "Този отговор не съществува";

        public const string TreePostReplyContentRequiredErrorMessage = "Липсва съдържание на отговора";

        public const string TreePostReplyMaxLengthRequiredErrorMessage = "Отговора е твърде дълъг";

        #endregion

        #region TreePostReplyReaction

        public const string TreePostReplyReactionNotFoundErrorMessage = "Тази реакция не съществува";

        #endregion

        #region Reactions

        public const string ReactionRequiredErrorMessage = "Реакцията ви няма тип";

        #endregion
    }
}
