import { useState } from "react";
import "./register.css";
import FormInput from "../components/FormInput";

const Register = () => {
    const [values, setValues] = useState({
        username: "",
        email: "",
        birthday: "",
        password: "",
        confirmPassword: "",
    });

    const inputs = [
        {
            id: 1,
            name: "username",
            type: "text",
            placeholder: "Username",
            errorMessage: "Username should be 3-16 cahracters and shouldn't include any special characters",
            label: "Username",
            pattern: "^[A-Za-z0-9]{3,16}$",
            required: true
        },
        {
            id: 2,
            name: "email",
            type: "email",
            placeholder: "Email",
            errorMessage: "It should be a valid email address",
            label: "Email",
            required: true
        },
        {
            id: 3,
            name: "birthday",
            type: "date",
            placeholder: "Birthday",
            errorMessage: "",
            label: "Birthday"
        },
        {
            id: 4,
            name: "phone",
            type: "text",
            placeholder: "Phone no.",
            errorMessage: "",
            label: "Phone"
        },
        {
            id: 5,
            name: "password",
            type: "password",
            placeholder: "Password",
            errorMessage: "Passoword should have minimum 8 characters, at least one letter and one number",
            label: "Password",
            // pattern: "^(?=.[A-Za-z])(?=.\d)[A-Za-z\d]{8,}$",
            required: true
        },
        {
            id: 6,
            name: "type",
            type: "text",
            placeholder: "Student or Teacher",
            errorMessage: "",
            label: "User Type",
            required: true
        }
        // {
        //   id:5,
        //   name:"confirmPassword",
        //   type:"password",
        //   placeholder:"Confirm Password",
        //   errorMessage: "Passwords don't match!",
        //   label:"Confirm Password",
        //   pattern: values.password,
        //   required: true
        // }
    ]

    const handleSubmit = (e) => {
        e.preventDefault();
    };

    const onChange = (e) => {
        setValues({ ...values, [e.target.name]: e.target.value });
    };

    console.log(values);

    return (
        <div className="app">

            <form onSubmit={handleSubmit}>
                <h1>Register</h1>
                {inputs.map((input) => (
                    <FormInput
                        key={input.id}
                        {...input}
                        value={values[input.name]}
                        onChange={onChange}
                    />
                ))}

                <button>Submit</button>
            </form>
        </div>

    );

};

export default Register;