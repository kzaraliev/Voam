import * as Yup from "yup";

import { OrderKeys } from "../../../utils/constants";

// min 5 characters, 1 upper case letter, 1 lower case letter, 1 numeric digit.
const passwordRules = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{5,}/;

// 10 digits, can include spaces, dashes, parentheses.
const phoneNumberRules = /^\(?([0-9]{3})\)?[- ]?([0-9]{3})[- ]?([0-9]{4})$/;

const orderValidation = Yup.object({
  [OrderKeys.Email]: Yup.string()
    .min(6, "Email must be 6 characters or more")
    .required("Enter your email")
    .email("Invalid email"),
  [OrderKeys.FirstName]: Yup.string()
    .min(2, "First name must be 2 characters or more")
    .required("Enter first name"),
  [OrderKeys.LastName]: Yup.string()
    .min(3, "Last name must be 3 characters or more")
    .required("Enter last name"),
  [OrderKeys.PhoneNumber]: Yup.string()
    .required("Enter your phone number")
    .matches(phoneNumberRules, "Phone number is not valid"),
  [OrderKeys.EcontOffice]: Yup.string()
    .min(10, "Enter more than 5 characters")
    .required(),
  [OrderKeys.City]: Yup.string()
    .min(3, "Enter more than 5 characters")
    .required(),
});

export default orderValidation;
