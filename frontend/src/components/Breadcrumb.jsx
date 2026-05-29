import React from 'react';

const Breadcrumb = ({
    currentPath,
    onNavigate
}) => {

    return (

        <nav className="mb-4">

            <ol className="breadcrumb">

                <li className="breadcrumb-item">

                    <button
                        className="btn btn-link text-decoration-none p-0"
                        onClick={() => onNavigate(-1)}
                    >
                        Root
                    </button>

                </li>

                {currentPath.map((folder, index) => (

                    <li
                        key={index}
                        className="breadcrumb-item"
                    >

                        <button
                            className="btn btn-link text-decoration-none p-0"
                            onClick={() => onNavigate(index)}
                        >
                            {folder}
                        </button>

                    </li>

                ))}

            </ol>

        </nav>
    );
};

export default Breadcrumb;