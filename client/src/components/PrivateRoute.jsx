import React from 'react';
import { Navigate } from 'react-router-dom';
import { getToken } from '../auth';

const PrivateRoute = ({ children, requiredRole }) => {
  const token = getToken();

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const userRole = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    if (requiredRole && userRole !== requiredRole) {
      return <Navigate to="/login" replace />;
    }

    return children;
  } catch (e) {
    return <Navigate to="/login" replace />;
  }
};

export default PrivateRoute;
