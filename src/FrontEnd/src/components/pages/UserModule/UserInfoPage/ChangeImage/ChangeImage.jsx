import React from 'react';
import Button from "../../../../common/Button/Button";
import FileInput from "../../../../common/FileInput/FileInput";

const ChangeImage = () => {
    const handleFilesUpload = () => {

    };

    return (
        <>
            <div className='text-center'>
                <Button type='DarkOutline'>
                    <div className="row" data-toggle="modal" data-target="#basicExampleModal">
                        <i className="fas fa-images mr-2"/>
                        Промени
                    </div>
                </Button>

            </div>
            <div className="modal fade" id="basicExampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                 aria-hidden="true">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="exampleModalLabel">Modal title</h5>
                            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div className="modal-body">
                            <FileInput onChange={handleFilesUpload}/>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-dismiss="modal">Затвори</button>
                            <button type="button" className="btn btn-primary">Промени</button>
                        </div>
                    </div>
                </div>
            </div>
            </>
    )
};

export default ChangeImage;