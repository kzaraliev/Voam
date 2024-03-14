import { useState, useMemo } from "react";

import Stepper from "./Stepper.jsx";
import StepOne from "./Steps/StepOne.jsx";
import StepTwo from "./Steps/StepTwo.jsx";
import StepThree from "./Steps/StepThree.jsx";

import styles from "./Checkout.module.css";

export default function Checkout() {
  const [activeStep, setActiveStep] = useState(1);

  const changeActiveStep = (stepValue) => {
    if (stepValue <= steps.length || stepValue >= 1) {
      setActiveStep(stepValue);
    }
  };

  const handleFormData = (newFormData) => {
    console.log(newFormData)
    const data = {
      fullName: newFormData.firstName + " " + newFormData.lastName,
      email: newFormData.email,
      phone: newFormData.phoneNumber,
      econt: newFormData.econtOffice,
      city: newFormData.city
    }
    localStorage.setItem("checkout-data", JSON.stringify(data));
  };

  const steps = useMemo(
    () => [
      {
        label: "Billing Details",
        value: 1,
        component: (
          <StepOne
            changeActiveStep={changeActiveStep}
            onFormDataChange={handleFormData}
          />
        ),
      },
      {
        label: "Order preview",
        value: 2,
        component: (
          <StepTwo
            changeActiveStep={changeActiveStep}
          />
        ),
      },
      {
        label: "Place order",
        value: 3,
        component: <StepThree changeActiveStep={changeActiveStep} />,
      },
    ],
    []
  );

  const activeComponent = useMemo(() => {
    return steps.find(({ value }) => value === activeStep)?.component || null;
  }, [activeStep, steps]);

  return (
    <div className={styles.checkoutContainer}>
      <Stepper
        steps={steps}
        activeStep={activeStep}
        changeActiveStep={changeActiveStep}
      />
      {activeComponent}
    </div>
  );
}
