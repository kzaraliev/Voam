import * as Yup from 'yup';

import { EditFormKeys } from '../../utils/constants';

const editProductValidation = Yup.object({
    [EditFormKeys.Name]: Yup.string()
        .min(2, 'Name must be 2 characters or more')
        .max(255, "Name must be less than 255 characters")
        .required('Enter product name'),
    [EditFormKeys.Description]: Yup.string()
        .min(5, 'Description must be 5 characters or more')
        .max(1500, "Description must be less than 1500 characters")
        .required("Description is required"),
    [EditFormKeys.Price]: Yup.number()
        .min(1, 'Price must be 1 characters or more')
        .max(1000, "Price must be less than 1000 characters")
        .required('Enter product price'),
    [EditFormKeys.SizeS]: Yup.number().integer("Must be an integer").required("This field is required"),
    [EditFormKeys.SizeM]: Yup.number().integer("Must be an integer").required("This field is required"),
    [EditFormKeys.SizeL]: Yup.number().integer("Must be an integer").required("This field is required"),
});

export default editProductValidation;