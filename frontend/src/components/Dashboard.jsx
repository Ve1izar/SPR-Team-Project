import React, { useEffect, useState } from "react";
import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:5000/api/storage"
});

export default function Dashboard() {

    const [items, setItems] = useState([]);

    const [path, setPath] = useState([]);

    const [folderName, setFolderName] = useState("");

    const [loading, setLoading] = useState(false);

    const currentPath = path.join("/");

    const loadItems = async () => {

        try {

            const res = await api.get("/", {
                params: {
                    path: currentPath
                }
            });

            console.log(res.data);

            setItems(res.data);

        } catch (e) {

            console.error(e);

            setItems([]);
        }
    };

    useEffect(() => {

        loadItems();

    }, [currentPath]);

    const uploadFile = async (e) => {

        const file = e.target.files[0];

        if (!file) return;

        try {

            setLoading(true);

            const formData = new FormData();

            formData.append("file", file);

            formData.append(
                "path",
                currentPath
            );

            await api.post(
                "/upload",
                formData,
                {
                    headers: {
                        "Content-Type":
                            "multipart/form-data"
                    }
                }
            );

            await loadItems();

        } catch (e) {

            console.error(e);

            alert("upload error");

        } finally {

            setLoading(false);
        }
    };

    const createFolder = async () => {

        if (!folderName.trim()) return;

        try {

            await api.post(
                "/create-folder",
                {
                    name: folderName,
                    path: currentPath
                }
            );

            setFolderName("");

            loadItems();

        } catch (e) {

            console.error(e);

            alert("folder error");
        }
    };

    const deleteItem = async (item) => {

        try {

            await api.delete(
                "/delete",
                {
                    params: {
                        name: item.name,
                        isFolder: item.isFolder,
                        path: currentPath
                    }
                }
            );

            loadItems();

        } catch (e) {

            console.error(e);

            alert("delete error");
        }
    };

    const openFolder = (folder) => {

        setPath([
            ...path,
            folder
        ]);
    };

    const goBack = (index) => {

        if (index === -1) {

            setPath([]);

            return;
        }

        setPath(
            path.slice(0, index + 1)
        );
    };

    const download = (fileName) => {

        window.open(
            `http://localhost:5000/api/storage/download/${fileName}?path=${currentPath}`,
            "_blank"
        );
    };

    return (

        <div
            style={{
                minHeight: "100vh",
                background:
                    "linear-gradient(135deg,#0f172a,#020617)",
                color: "white",
                padding: "30px",
                fontFamily: "Arial"
            }}
        >

            <div
                style={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                    marginBottom: "30px"
                }}
            >

                <div>

                    <h1
                        style={{
                            fontSize: "42px",
                            margin: 0
                        }}
                    >
                        ☁ Local Drive
                    </h1>

                    <div
                        style={{
                            color: "#94a3b8"
                        }}
                    >
                        Cloud Storage
                    </div>

                </div>

                <label
                    style={{
                        background:
                            "linear-gradient(135deg,#3b82f6,#2563eb)",
                        padding:
                            "14px 24px",
                        borderRadius: "16px",
                        cursor: "pointer",
                        fontWeight: "bold",
                        boxShadow:
                            "0 0 20px rgba(37,99,235,.4)"
                    }}
                >

                    {
                        loading
                            ? "Uploading..."
                            : "+ Upload"
                    }

                    <input
                        hidden
                        type="file"
                        onChange={uploadFile}
                    />

                </label>

            </div>

            <div
                style={{
                    background:
                        "rgba(255,255,255,.05)",
                    border:
                        "1px solid rgba(255,255,255,.1)",
                    borderRadius: "20px",
                    padding: "20px",
                    marginBottom: "20px",
                    backdropFilter: "blur(20px)"
                }}
            >

                <div
                    style={{
                        display: "flex",
                        gap: "10px",
                        flexWrap: "wrap"
                    }}
                >

                    <button
                        onClick={() =>
                            goBack(-1)
                        }
                        style={breadcrumbStyle}
                    >
                        Root
                    </button>

                    {path.map((p, i) => (

                        <button
                            key={i}
                            onClick={() =>
                                goBack(i)
                            }
                            style={breadcrumbStyle}
                        >
                            {p}
                        </button>

                    ))}

                </div>

            </div>

            <div
                style={{
                    display: "flex",
                    gap: "12px",
                    marginBottom: "30px"
                }}
            >

                <input
                    value={folderName}
                    onChange={(e) =>
                        setFolderName(
                            e.target.value
                        )
                    }
                    placeholder="Folder name"
                    style={{
                        flex: 1,
                        background:
                            "rgba(255,255,255,.08)",
                        border:
                            "1px solid rgba(255,255,255,.1)",
                        color: "white",
                        padding: "16px",
                        borderRadius: "16px",
                        outline: "none"
                    }}
                />

                <button
                    onClick={createFolder}
                    style={{
                        background:
                            "linear-gradient(135deg,#22c55e,#16a34a)",
                        border: "none",
                        color: "white",
                        padding:
                            "0 25px",
                        borderRadius: "16px",
                        fontWeight: "bold"
                    }}
                >
                    + Folder
                </button>

            </div>

            <div
                style={{
                    display: "grid",
                    gridTemplateColumns:
                        "repeat(auto-fill,minmax(260px,1fr))",
                    gap: "20px"
                }}
            >

                {items.map((item, i) => (

                    <div
                        key={i}
                        style={{
                            background:
                                "rgba(255,255,255,.05)",
                            border:
                                "1px solid rgba(255,255,255,.08)",
                            borderRadius: "24px",
                            padding: "24px",
                            backdropFilter:
                                "blur(16px)",
                            transition: ".2s"
                        }}
                    >

                        <div
                            style={{
                                fontSize: "72px",
                                marginBottom: "12px"
                            }}
                        >
                            {
                                item.isFolder
                                    ? "📁"
                                    : "📄"
                            }
                        </div>

                        <div
                            style={{
                                fontWeight: "bold",
                                fontSize: "18px",
                                marginBottom: "10px",
                                wordBreak: "break-word"
                            }}
                        >
                            {item.name}
                        </div>

                        <div
                            style={{
                                color: "#94a3b8",
                                marginBottom: "20px"
                            }}
                        >
                            {
                                item.isFolder
                                    ? "Folder"
                                    : `${(
                                        item.size / 1024
                                    ).toFixed(2)} KB`
                            }
                        </div>

                        {
                            item.isFolder ? (

                                <button
                                    onClick={() =>
                                        openFolder(item.name)
                                    }
                                    style={blueBtn}
                                >
                                    Open
                                </button>

                            ) : (

                                <button
                                    onClick={() =>
                                        download(item.name)
                                    }
                                    style={purpleBtn}
                                >
                                    Download
                                </button>

                            )
                        }

                        <button
                            onClick={() =>
                                deleteItem(item)
                            }
                            style={redBtn}
                        >
                            Delete
                        </button>

                    </div>

                ))}

            </div>

        </div>
    );
}

const breadcrumbStyle = {
    background: "rgba(255,255,255,.08)",
    border: "none",
    color: "white",
    padding: "10px 16px",
    borderRadius: "12px",
    cursor: "pointer"
};

const blueBtn = {
    width: "100%",
    background:
        "linear-gradient(135deg,#3b82f6,#2563eb)",
    border: "none",
    color: "white",
    padding: "12px",
    borderRadius: "14px",
    marginBottom: "10px",
    cursor: "pointer",
    fontWeight: "bold"
};

const purpleBtn = {
    width: "100%",
    background:
        "linear-gradient(135deg,#a855f7,#7e22ce)",
    border: "none",
    color: "white",
    padding: "12px",
    borderRadius: "14px",
    marginBottom: "10px",
    cursor: "pointer",
    fontWeight: "bold"
};

const redBtn = {
    width: "100%",
    background:
        "linear-gradient(135deg,#ef4444,#dc2626)",
    border: "none",
    color: "white",
    padding: "12px",
    borderRadius: "14px",
    cursor: "pointer",
    fontWeight: "bold"
};