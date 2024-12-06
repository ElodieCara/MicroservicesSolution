import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { usePatients } from "../../hooks/usePatients";

const EditPatientForm = () => {
    const { id } = useParams();
    const { patients, updatePatient } = usePatients();
    const navigate = useNavigate();
    const [formData, setFormData] = useState(null);

    useEffect(() => {
        const patient = patients.find((p) => p.id === parseInt(id));
        if (patient) setFormData(patient);
    }, [id, patients]);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        await updatePatient(id, formData);
        navigate("/"); // Retour à la liste des patients
    };

    if (!formData) return <p>Loading patient details...</p>;

    return (
        <form onSubmit={handleSubmit}>
            <h2>Modifier un patient</h2>
            <input name="firstName" value={formData.firstName} onChange={handleInputChange} />
            <input name="lastName" value={formData.lastName} onChange={handleInputChange} />
            <input name="dateOfBirth" type="date" value={formData.dateOfBirth} onChange={handleInputChange} />
            <input name="gender" value={formData.gender} onChange={handleInputChange} />
            <input name="address" value={formData.address} onChange={handleInputChange} />
            <input name="phoneNumber" value={formData.phoneNumber} onChange={handleInputChange} />
            <button type="submit">Mettre à jour</button>
        </form>
    );
};

export default EditPatientForm;
