import * as Yup from 'yup';

import { RegisterFormKeys } from '../../utils/constants';

// min 5 characters, 1 upper case letter, 1 lower case letter, 1 numeric digit.
const passwordRules = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{5,}/;

// 10 digits, can include spaces, dashes, parentheses.
const phoneNumberRules = /^\(?([0-9]{3})\)?[- ]?([0-9]{3})[- ]?([0-9]{4})$/;

const namesRules = /^[A-Za-z]+$/;

const registerValidation = Yup.object({
    [RegisterFormKeys.Email]: Yup.string()
        .min(6, 'Email must be 6 characters or more')
        .required('Enter your email')
        .email('Invalid email'),
    [RegisterFormKeys.FirstName]: Yup.string()
        .min(2, 'First name must be 2 characters or more')
        .trim('First name must not include leading or trailing spaces')
        .required('Enter first name')
        .matches(namesRules, "Only letters allowed. No numbers or special characters."),
    [RegisterFormKeys.LastName]: Yup.string()
        .min(3, 'Last name must be 3 characters or more')
        .trim('First name must not include leading or trailing spaces')
        .required('Enter last name')
        .matches(namesRules, "Only letters allowed. No numbers or special characters."),
    [RegisterFormKeys.Password]: Yup.string()
        .min(5, 'Password must be 5 characters or more')
        .required('Enter your password')
        .matches(
            passwordRules,
            'Include uppercase, lowercase, and numeric characters in your password'
    ),
        [RegisterFormKeys.PhoneNumber]: Yup.string()
        .required('Enter your phone number')
        .matches(
            phoneNumberRules,
            'Phone number is not valid'
        ),
});

export default registerValidation;