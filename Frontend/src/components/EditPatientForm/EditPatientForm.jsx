//import React from 'react';

//const EditPatientForm = ({ editPatient, onEditPatient, onCancelEdit }) => {
//    const handleInputChange = (e) => {
//        const { name, value } = e.target;
//        onEditPatient({ ...editPatient, [name]: value });
//    };

//    const handleSubmit = (e) => {
//        e.preventDefault();
//        onEditPatient(editPatient);
//    };

//    return (
//        <form onSubmit={handleSubmit}>
//            <input
//                type="text"
//                name="firstName"
//                value={editPatient.firstName}
//                onChange={handleInputChange}
//            />
//            <input
//                type="text"
//                name="lastName"
//                value={editPatient.lastName}
//                onChange={handleInputChange}
//            />
//            <input
//                type="date"
//                name="dateOfBirth"
//                value={editPatient.dateOfBirth}
//                onChange={handleInputChange}
//            />
//            <input
//                type="text"
//                name="gender"
//                value={editPatient.gender}
//                onChange={handleInputChange}
//            />
//            <input
//                type="text"
//                name="address"
//                value={editPatient.address}
//                onChange={handleInputChange}
//            />
//            <input
//                type="text"
//                name="phoneNumber"
//                value={editPatient.phoneNumber}
//                onChange={handleInputChange}
//            />
//            <button type="submit">Mettre à jour</button>
//            <button type="button" onClick={onCancelEdit}>Annuler</button>
//        </form>
//    );
//};

//export default EditPatientForm;
