// src/App.jsx
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./components/Auth/Login";
import PatientList from "./components/PatientList/PatientList";
import AddPatientForm from "./components/AddPatientForm/AddPatientForm";
import EditPatientForm from "./components/EditPatientForm/EditPatientForm";
import PatientDetail from "./components/PatientDetail/PatientDetail";
import ProtectedRoute from "./components/ProtectedRoute/ProtectedRoute";

const App = () => {
    return (
        <Router>
            <div>
                <h1>Patient Management System</h1>
                <Routes>
                    <Route path="/login" element={<Login />} />

                    <Route
                        path="/"
                        element={
                            <ProtectedRoute>
                                <PatientList />
                            </ProtectedRoute>
                        }
                    />

                    <Route
                        path="/add-patient"
                        element={
                            <ProtectedRoute>
                                <AddPatientForm />
                            </ProtectedRoute>
                        }
                    />

                    <Route
                        path="/edit-patient/:id"
                        element={
                            <ProtectedRoute>
                                <EditPatientForm />
                            </ProtectedRoute>
                        }
                    />

                    <Route
                        path="/patient/:id"
                        element={
                            <ProtectedRoute>
                                <PatientDetail />
                            </ProtectedRoute>
                        }
                    />
                </Routes>
            </div>
        </Router>
    );
};

export default App;
