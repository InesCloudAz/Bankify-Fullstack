// App.jsx (Skriv om det till detta)

import React from "react";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import Login from "./pages/Login";
import PrivateRoute from "./components/PrivateRoute";
import AdminDashboard from "./pages/AdminDashboard";
import CreateCustomer from "./pages/CreateCustomer";
import NewLoan from "./pages/NewLoan";
import Dashboard from "./pages/Dashboard"; // flytta ut Dashboard till en egen komponent!
import Accounts from "./pages/Accounts";
import Transfers from "./pages/Transfers";
import CreateAccount from "./pages/CreateAccount";

export default function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        
        <Route path="/" element={
          <PrivateRoute requiredRole="Customer">
            <Dashboard />
          </PrivateRoute>
        } />

        <Route path="/accounts" element={
          <PrivateRoute requiredRole="Customer">
            <Accounts />
          </PrivateRoute>
        } />

        <Route path="/transfers" element={
          <PrivateRoute requiredRole="Customer">
            <Transfers />
          </PrivateRoute>
        } />

        <Route path="/create-account" element={
          <PrivateRoute requiredRole="Customer">
            <CreateAccount />
          </PrivateRoute>
        } />

        <Route path="/admin-dashboard" element={
          <PrivateRoute requiredRole="Admin">
            <AdminDashboard />
          </PrivateRoute>
        } />

        <Route path="/create-customer" element={
          <PrivateRoute requiredRole="Admin">
            <CreateCustomer />
          </PrivateRoute>
        } />

        <Route path="/new-loan" element={
          <PrivateRoute requiredRole="Admin">
            <NewLoan />
          </PrivateRoute>
        } />
      </Routes>
    </Router>
  );
}
