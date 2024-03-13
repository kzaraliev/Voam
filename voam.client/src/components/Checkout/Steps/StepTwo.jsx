export default function StepTwo({changeActiveStep}) {
    return(<>
    <h1>Hello</h1>
    <button onClick={() => changeActiveStep(3)}>Next page</button>
    </>)
}