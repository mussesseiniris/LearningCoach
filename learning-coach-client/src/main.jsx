import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.jsx";
import  LoginPage  from "./page/loginPage.jsx";
import RegisterPage from "./page/RegisterPage.jsx";
import { BrowserRouter, Routes, Route } from 'react-router-dom'


createRoot(document.getElementById("root")).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/homePage" element={<App />} />
        <Route path="/signUp" element={<RegisterPage />} />
      </Routes>
    </BrowserRouter>
  </StrictMode>,
);
