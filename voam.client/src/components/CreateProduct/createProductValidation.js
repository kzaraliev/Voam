import * as Yup from 'yup';

import { CreateFormKeys } from '../../utils/constants';

const createProductValidation = Yup.object({
    [CreateFormKeys.Name]: Yup.string()
        .min(2, 'Name must be 2 characters or more')
        .required('Enter product name'),
    [CreateFormKeys.Description]: Yup.string()
        .min(5, 'Description must be 5 characters or more'),
    [CreateFormKeys.Price]: Yup.number()
        .min(1, 'Password must be 1 characters or more')
        .required('Enter product price'),
    [CreateFormKeys.SizeS]: Yup.number().integer("Must be an integer"),
    [CreateFormKeys.SizeM]: Yup.number().integer("Must be an integer"),
    [CreateFormKeys.SizeL]: Yup.number().integer("Must be an integer"),
});

export default createProductValidation;