import { useState } from "react";
import "./RegisterPage.css";
import { useNavigate } from "react-router-dom";
export default function RegisterPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErorMes] = useState("");
  const [userName, setUserName] = useState("");

  const navigate = useNavigate();

  async function handleSignUp() {
    var result = await fetch("/api/auth/register", {
      method: "Post",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        email: email,
        name:userName,
        password: password,
      }),
    });
    if (result.ok) {
     navigate("/login");
    } else {
      setErorMes("Invalid Email or password");
    }
  }

  return (
    <div className="RegisterHome">
    <div className="Register">
      <h2>LEARNING COACH</h2>
      <h3>Sign up:)</h3>
      <p>Email</p>
      <input
        type="text"
        placeholder=""
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />

        <p>User Name</p>
      <input
        type="text"
        placeholder=""
        value={userName}
        onChange={(e) => setUserName(e.target.value)}
      />
      <p>Password</p>
      <input
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      {errorMessage && <p className="setError">{errorMessage}</p>}
      <button onClick={() => handleSignUp()}>Sign up</button>

    </div>
    </div>
  );
}
