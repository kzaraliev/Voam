import { FaClipboardList } from "react-icons/fa6";
import { FaShoppingCart } from "react-icons/fa";
import { RiShoppingBag3Fill } from "react-icons/ri";
import { HiOutlineCheck } from "react-icons/hi2";

import styles from "./Checkout.module.css";

export default function Stepper({ activeStep, steps }) {
  const isStepComplete = (currentStep) => activeStep > currentStep;
  return (
    <div className={styles.stepperContainer}>
      <h1 className={styles.stepperTitle}>Checkout</h1>
      <div>
        <ol className={styles.stepperList}>
          {steps.map(({ value, label }) => {
            return (
              <li key={value} className={styles.step}>
                <div
                  className={`${
                    isStepComplete(value)
                      ? styles.isCompleted
                      : styles.isNotCompleted
                  }`}
                >
                  <span className={styles.icon}>
                    {value === 1 ? (
                      isStepComplete(value) ? <HiOutlineCheck /> : <FaClipboardList/>
                    ) : value === 2 ? (
                      <FaShoppingCart />
                    ) : value === 3 ? (
                      <RiShoppingBag3Fill />
                    ) : null}
                  </span>
                </div>
                <div>{label}</div>
              </li>
            );
          })}
        </ol>
      </div>
    </div>
  );
}
