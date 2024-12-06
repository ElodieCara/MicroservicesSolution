import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { usePatients } from "../../hooks/usePatients";

const AddPatientForm = () => {
    const { addPatient } = usePatients();
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        firstName: "",
        lastName: "",
        dateOfBirth: "",
        gender: "",
        address: "",
        phoneNumber: "",
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        await addPatient(formData);
        navigate("/"); // Retour à la liste des patients
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Ajouter un patient</h2>
            <input name="firstName" placeholder="Prénom" onChange={handleInputChange} />
            <input name="lastName" placeholder="Nom" onChange={handleInputChange} />
            <input name="dateOfBirth" type="date" onChange={handleInputChange} />
            <input name="gender" placeholder="Genre" onChange={handleInputChange} />
            <input name="address" placeholder="Adresse" onChange={handleInputChange} />
            <input name="phoneNumber" placeholder="Téléphone" onChange={handleInputChange} />
            <button type="submit">Ajouter</button>
        </form>
    );
};

export default AddPatientForm;
