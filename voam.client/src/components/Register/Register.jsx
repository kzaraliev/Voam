import { useState, useContext } from "react";
import { Link } from "react-router-dom";
import Form from "react-bootstrap/Form";
import FloatingLabel from "react-bootstrap/FloatingLabel";
import { useFormik } from "formik";
import { FaEye, FaEyeSlash } from "react-icons/fa";

import styles from "../../styles/FormStyles.module.css";
import Path from "../../utils/paths";
// import AuthContext from "../../context/authContext";
import { RegisterFormKeys } from "../../utils/constants";
import registerValidation from "./registerValidation";

const initialValues = {
  [RegisterFormKeys.Email]: "",
  [RegisterFormKeys.Username]: "",
  [RegisterFormKeys.Password]: "",
  [RegisterFormKeys.ConfirmPassword]: "",
};

export default function Register() {
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const {
    values,
    errors,
    touched,
    handleBlur,
    handleChange,
    handleSubmit,
    isSubmitting,
  } = useFormik({
    initialValues,
    validationSchema: registerValidation,
    onSubmit,
  });

  //const { registerSubmitHandler } = useContext(AuthContext);

  async function onSubmit(values) {
    //await registerSubmitHandler(values);
  }

  const passwordVisibilityToggle = () => {
    setShowPassword(!showPassword);
  };

  const confirmPasswordVisibilityToggle = () => {
    setShowConfirmPassword(!showConfirmPassword);
  };

  return (
    <div className={styles.containerForm}>
      <Form className={styles.form} onSubmit={handleSubmit}>
        <h1 className={styles.title}>Register</h1>

        <Form.Group className="mb-3">
          {errors[RegisterFormKeys.Username] &&
            touched[RegisterFormKeys.Username] && (
              <p className={styles.invalid}>
                {errors[RegisterFormKeys.Username]}
              </p>
            )}
          <FloatingLabel
            htmlFor={RegisterFormKeys.Username}
            label="Full name"
            className="mb-3"
          >
            <Form.Control
              type="text"
              id="username"
              name={RegisterFormKeys.Username}
              placeholder="Enter username"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values[RegisterFormKeys.Username]}
            />
          </FloatingLabel>
        </Form.Group>

        <Form.Group className="mb-3">
          {errors[RegisterFormKeys.Email] &&
            touched[RegisterFormKeys.Email] && (
              <p className={styles.invalid}>{errors[RegisterFormKeys.Email]}</p>
            )}
          <FloatingLabel
            htmlFor={RegisterFormKeys.Email}
            label="Email address"
            className="mb-3"
          >
            <Form.Control
              type="text"
              id="email"
              name={RegisterFormKeys.Email}
              placeholder="Enter email"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values[RegisterFormKeys.Email]}
            />
          </FloatingLabel>
        </Form.Group>

        <Form.Group className="mb-3">
          {errors[RegisterFormKeys.Password] &&
            touched[RegisterFormKeys.Password] && (
              <p className={styles.invalid}>
                {errors[RegisterFormKeys.Password]}
              </p>
            )}
          <div className={styles.inputContainer}>
            <FloatingLabel
              htmlFor={RegisterFormKeys.Password}
              label="Password"
              className="mb-3"
            >
              <Form.Control
                type={showPassword ? "text" : "password"}
                name={RegisterFormKeys.Password}
                id="password"
                placeholder="Enter password"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values[RegisterFormKeys.Password]}
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

        <Link to={Path.Login} className={styles.ref}>
          *Already have an account?
        </Link>
        <button
          className={styles.submitButton}
          type="submit"
          disabled={isSubmitting}
        >
          Register
        </button>
      </Form>
    </div>
  );
}
