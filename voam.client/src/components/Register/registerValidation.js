import * as Yup from 'yup';

import { RegisterFormKeys } from '../../utils/constants';

const passwordRules = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{5,}/;
// min 5 characters, 1 upper case letter, 1 lower case letter, 1 numeric digit.
// const regMatch = /((https?):\/\/)?(www.)?[a-z0-9]+(\.[a-z]{2,}){1,3}(#?\/?[a-zA-Z0-9#]+)*\/?(\?[a-zA-Z0-9-_]+=[a-zA-Z0-9-%]+&?)?$/; - for some reason the URL validation makes the whole application to crash

const today = new Date();

const registerValidation = Yup.object({
    [RegisterFormKeys.Email]: Yup.string()
        .min(6, 'Email must be at least 6 characters long')
        .required('Enter your email')
        .email('Invalid email'),
    [RegisterFormKeys.Username]: Yup.string()
        .min(5, 'Username must be at least 5 characters long')
        .required('Enter username'),
    [RegisterFormKeys.Password]: Yup.string()
        .min(5, 'Password must be at least 5 characters long')
        .required('Enter your password')
        .matches(
            passwordRules,
            'Password must have at least 1 upper case letter, 1 lower case letter and 1 numeric digit'
        ),
    [RegisterFormKeys.ConfirmPassword]: Yup.string()
        .min(5, 'Confirm password must be at least 5 characters long')
        .oneOf(
            [Yup.ref(RegisterFormKeys.Password), null],
            'Passwords miss match'
        )
        .required('Confrim your password'),
});

export default registerValidation;