import React from 'react';
import Button from "../../../../common/Button/Button";
import FileInput from "../../../../common/FileInput/FileInput";

const ChangeImage = ({onSubmit}) => {
    const handleFilesUpload = () => {

    };

    return (
        <>
            <div className='text-center'>
                <Button type='DarkOutline'>
                    <div className="row" data-toggle="modal" data-target="#profilePictureInputContainer">
                        <i className="fas fa-images mr-2"/>
                        Промени
                    </div>
                </Button>

            </div>
            <div className="modal fade" id="profilePictureInputContainer"  role="dialog" aria-labelledby="profilePictureInputContainer"
                 aria-hidden="true">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="profilePictureInputContainer">Добави снимка</h5>
                            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div className="modal-body">
                            <FileInput onChange={handleFilesUpload}/>
                        </div>
                        <div className="modal-footer">
                            <Button type='Red'>
                                <div aria-label="Close" data-dismiss="modal">Затвори</div>
                            </Button>
                            <Button type='Green' onClick={onSubmit}>Промени</Button>
                        </div>
                    </div>
                </div>
            </div>
            </>
    )
};

export default ChangeImage;