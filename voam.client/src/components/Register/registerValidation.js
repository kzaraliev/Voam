import * as Yup from 'yup';

import { RegisterFormKeys } from '../../utils/constants';

const passwordRules = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{5,}/;
// min 5 characters, 1 upper case letter, 1 lower case letter, 1 numeric digit.

const registerValidation = Yup.object({
    [RegisterFormKeys.Email]: Yup.string()
        .min(6, 'Email must be 6 characters or more')
        .required('Enter your email')
        .email('Invalid email'),
    [RegisterFormKeys.Username]: Yup.string()
        .min(5, 'Username must be 5 characters or more')
        .required('Enter username'),
    [RegisterFormKeys.Password]: Yup.string()
        .min(5, 'Password must be 5 characters or more')
        .required('Enter your password')
        .matches(
            passwordRules,
            'Include uppercase, lowercase, and numeric characters in your password'
        ),
});

export default registerValidation;