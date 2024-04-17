import { useContext, useState } from "react";
import { Link } from "react-router-dom";
import Form from "react-bootstrap/Form";
import FloatingLabel from 'react-bootstrap/FloatingLabel';
import { useFormik } from "formik";
import { FaEye, FaEyeSlash } from "react-icons/fa";

import styles from "../../styles/FormStyles.module.css";
import Path from "../../utils/paths";
import AuthContext from "../../context/authContext";
import { LoginFormKeys } from "../../utils/constants";

const initialValues = {
    [LoginFormKeys.Email]: "",
    [LoginFormKeys.Password]: "",
};

export default function Login() {
    const [showPassword, setShowPassword] = useState(false);
    const [serverError, setServerError] = useState("");

    const { values, handleSubmit, handleChange, handleBlur, isSubmitting } =
        useFormik({
            initialValues,
            onSubmit,
        });

    const { loginSubmitHandler } = useContext(AuthContext);

    async function onSubmit(values) {
        try {
            await loginSubmitHandler(values);
        } catch (error) {
            setServerError(error.message);
        }
    }

    const passwordVisibilityToggle = () => {
        setShowPassword(!showPassword);
    };

    return (
        <div className={styles.containerForm}>
            <Form className={styles.form} onSubmit={handleSubmit}>
                <h1 className={styles.title}>Login</h1>
                {serverError && (
                    <div>
                        <p className={styles.invalid}>{serverError}</p>
                    </div>
                )}
                <Form.Group className="mb-3">
                    <FloatingLabel
                        htmlFor="email"
                        label="Email address"
                        className="mb-3">
                        <Form.Control
                            type="text"
                            id="email"
                            name={LoginFormKeys.Email}
                            onChange={handleChange}
                            value={values[LoginFormKeys.Email]}
                            onBlur={handleBlur}
                            placeholder="name@example.com"
                            required={true}
                        />
                    </FloatingLabel>
                </Form.Group>
                <Form.Group className="mb-3">
                    <div className={styles.inputContainer}>
                        <FloatingLabel
                            htmlFor="password"
                            label="Password"
                            className="mb-3">
                        <Form.Control
                            type={showPassword ? "text" : "password"}
                            id="password"
                            name={LoginFormKeys.Password}
                            placeholder="Enter password"
                            onChange={handleChange}
                            value={values[LoginFormKeys.Password]}
                            required={true}
                            />
                        </FloatingLabel>
                        <div
                            className={styles.showPassword}
                            onClick={passwordVisibilityToggle}
                        >
                            {showPassword ? <FaEyeSlash /> : <FaEye />}
                        </div>
                    </div>
                </Form.Group>
                <Link to={Path.Register} className={styles.ref}>
                    *Don't have an account?
                </Link>
                <button
                    className={styles.submitButton}
                    type="submit"
                    disabled={isSubmitting}
                >
                    Login
                </button>
            </Form>
        </div>
    );
}
