import { useState } from "react";
import "./loginPage.css";
import { useNavigate } from "react-router-dom";
export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErorMes] = useState("");

  const navigate = useNavigate();

  async function handleLogin() {
    var result = await fetch("http://localhost:5138/api/auth/login", {
      method: "Post",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        email: email,
        password: password,
      }),
    });
    if (result.ok) {
      var data = await result.json();
      //store token
      localStorage.setItem("token", data.message);
      navigate("/");
    } else {
      setErorMes("Invalid Email or password");
    }
  }

  return (
    <div className="login">
      <h2>LEARNING COACH</h2>
      <h3>Login to your account :)</h3>
      <p>Email</p>
      <input
        type="text"
        placeholder=""
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <p>Password</p>
      <input
        type="text"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      {errorMessage && <p className="setError">{errorMessage}</p>}
      <button onClick={() => handleLogin()}>Log In</button>

      <div className="signUp">
        <p>Not registered yet?</p>
        <button>Sign Up :)</button>
      </div>
    </div>
  );
}
