import React, { useState } from 'react';
import { createNewCustomer } from '../services/api';

const CreateCustomer = () => {
  const [customer, setCustomer] = useState({
    customerID: 0,
    gender: '',
    givenname: '',
    surname: '',
    streetaddress: '',
    city: '',
    zipcode: '',
    country: '',
    countryCode: '',
    birthday: '',
    telephonecountrycode: '',
    telephonenumber: '',
    emailaddress: '',
    role: ''
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCustomer(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createNewCustomer(customer);
      alert("Ny kund skapad!");
      setCustomer({ ...customer, customerID: 0 }); // Reset eller hantera efter behov
    } catch (error) {
      alert("Misslyckades att skapa kund: " + error.message);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="p-4 space-y-4">
      {Object.keys(customer).map((key) => (
        <div key={key}>
          <label>{key}</label>
          <input
            name={key}
            type={key.includes("date") || key === "birthday" ? "date" : "text"}
            value={customer[key]}
            onChange={handleChange}
            className="border rounded p-1 w-full"
          />
        </div>
      ))}
      <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded">Skapa kund</button>
    </form>
  );
};

export default CreateCustomer;
