import React, { useState } from 'react';
import { createNewLoan } from '../services/api';

const NewLoan = () => {
  const [loan, setLoan] = useState({
    accountID: 0,
    customerID: 0,
    date: '',
    amount: 0,
    duration: 0,
    payments: 0,
    status: '',
    typeName: ''
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setLoan(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createNewLoan(loan);
      alert("Lån skapat!");
      setLoan({ ...loan, accountID: 0, customerID: 0 }); // Reset vid behov
    } catch (error) {
      alert("Misslyckades att skapa lån: " + error.message);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="p-4 space-y-4">
      {Object.keys(loan).map((key) => (
        <div key={key}>
          <label>{key}</label>
          <input
            name={key}
            type={key === 'date' ? 'date' : typeof loan[key] === 'number' ? 'number' : 'text'}
            value={loan[key]}
            onChange={handleChange}
            className="border rounded p-1 w-full"
          />
        </div>
      ))}
      <button type="submit" className="bg-green-500 text-white px-4 py-2 rounded">Skapa lån</button>
    </form>
  );
};

export default NewLoan;
