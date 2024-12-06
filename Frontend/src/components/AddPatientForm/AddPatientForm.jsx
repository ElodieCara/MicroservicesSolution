//import React, { useState } from 'react';

//const AddPatientForm = ({ onAddPatient }) => {
//    const [newPatient, setNewPatient] = useState({
//        firstName: "",
//        lastName: "",
//        dateOfBirth: "",
//        gender: "",
//        address: "",
//        phoneNumber: ""
//    });

//    const handleInputChange = (e) => {
//        const { name, value } = e.target;
//        setNewPatient({ ...newPatient, [name]: value });
//    };

//    const handleSubmit = (e) => {
//        e.preventDefault();
//        onAddPatient(newPatient);
//        setNewPatient({
//            firstName: "",
//            lastName: "",
//            dateOfBirth: "",
//            gender: "",
//            address: "",
//            phoneNumber: ""
//        });
//    };

//    return (
//        <form onSubmit={handleSubmit}>
//            <input
//                type="text"
//                name="firstName"
//                placeholder="Prénom"
//                value={newPatient.firstName}
//                onChange={handleInputChange}
//            />
//            <input
//                type="text"
//                name="lastName"
//                placeholder="Nom"
//                value={newPatient.lastName}
//                onChange={handleInputChange}
//            />
//            <input
//                type="date"
//                name="dateOfBirth"
//                placeholder="Date de naissance"
//                value={newPatient.dateOfBirth}
//                onChange={handleInputChange}
//            />
//            <input
//                type="text"
//                name="gender"
//                placeholder="Genre"
//                value={newPatient.gender}
//                onChange={handleInputChange}
//            />
//            <input
//                type="text"
//                name="address"
//                placeholder="Adresse"
//                value={newPatient.address}
//                onChange={handleInputChange}
//            />
//            <input
//                type="text"
//                name="phoneNumber"
//                placeholder="Téléphone"
//                value={newPatient.phoneNumber}
//                onChange={handleInputChange}
//            />
//            <button type="submit">Ajouter</button>
//        </form>
//    );
//};

//export default AddPatientForm;
