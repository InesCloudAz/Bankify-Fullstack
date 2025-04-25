import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import Accounts from './pages/Accounts';
import Transfers from './pages/Transfers';
import Transactions from './pages/Transactions';
import PrivateRoute from './components/PrivateRoute';
import CreateAccount from './pages/CreateAccount';
import AdminDashboard from './pages/AdminDashboard';
import CreateCustomer from './pages/CreateCustomer';
import NewLoan from './pages/NewLoan';

function App() {
  return (
    <Router>
      <Routes>
      {/* Customer routes */}
  <Route
    path="/"
    element={
      <PrivateRoute requiredRole="Customer">
        <Dashboard />
      </PrivateRoute>
    }
  />
  <Route
    path="/accounts"
    element={
      <PrivateRoute requiredRole="Customer">
        <Accounts />
      </PrivateRoute>
    }
  />
  <Route
    path="/transfers"
    element={
      <PrivateRoute requiredRole="Customer">
        <Transfers />
      </PrivateRoute>
    }
  />
  <Route
    path="/transactions"
    element={
      <PrivateRoute requiredRole="Customer">
        <Transactions />
      </PrivateRoute>
    }
  />
  <Route
    path="/create-account"
    element={
      <PrivateRoute requiredRole="Customer">
        <CreateAccount />
      </PrivateRoute>
    }
  />

  {/* Admin routes */}
  <Route
    path="/admin-dashboard"
    element={
      <PrivateRoute requiredRole="Admin">
        <AdminDashboard />
      </PrivateRoute>
    }
  />
  <Route
    path="/create-customer"
    element={
      <PrivateRoute requiredRole="Admin">
        <CreateCustomer />
      </PrivateRoute>
    }
  />
  <Route
    path="/new-loan"
    element={
      <PrivateRoute requiredRole="Admin">
        <NewLoan />
      </PrivateRoute>
    }
  />

  {/* Login always public */}
  <Route path="/login" element={<Login />} />
</Routes>

      
    </Router>
  );
}

export default App;
